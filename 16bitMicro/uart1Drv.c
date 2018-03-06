// Ersin GONUL //
// Inertial Navigation System for Unmanned Aerial Vehicles //
#include "p33FJ256GP710a.h"
#include<string.h> 
//#if defined(__dspic33f__)
//#include "p33fxxxx.h"
//#elif defined(__pic24h__)
//#include "p24fxxxx.h"
//#endif
//
#include<stdio.h>
#define  MAX_CHNUM	 			14		// Highest Analog input number in Channel Scan
#define  SAMP_BUFF_SIZE	 		2		// Size of the input buffer per analog input
#define  NUM_CHS2SCAN			12		// Number of channels enabled for channel scan
#define  IMU_TX_DATA_BYTE_SIZE 43
#define  GPS_TX_DATA_BYTE_SIZE 43
#define mask 0x0F						//For GPS Data Parse


// Number of locations for ADC buffer = 14 (AN0 to AN13) x 8 = 112 words
// Align the buffer to 128 words or 256 bytes. This is needed for peripheral indirect mode
int  ADCBufferA[MAX_CHNUM+1][SAMP_BUFF_SIZE] __attribute__((space(dma),aligned(512)));
int  ADCBufferB[MAX_CHNUM+1][SAMP_BUFF_SIZE] __attribute__((space(dma),aligned(512)));
void ProcessADCSamples(int * AdcBuffer);


//Align Buffer for IMU TX
struct
{	unsigned int ADCSection[64];
	unsigned int GPSSection[64];
} TXBuffer __attribute__((space(dma)));


int  RX_BufferA[8] __attribute__((space(dma),aligned(8)));
int  RX_BufferB[8] __attribute__((space(dma),aligned(8)));


struct
{	char Buffer1[128];
 char Buffer2[128];
	char Buffer3[128];
} GPS __attribute__((space(dma)));

//int  GPSBuffer __attribute__((space(dma),aligned(256)));


unsigned int accx=0, accy=0, accz=0, gyrox=0, gyroy=0, gyroz=0, 
			 magx=0, magy=0, magz=0, ias=3, alt=2, temp=1,
			 b_accx=0, b_accy=0, b_accz=0, b_gyrox=0, b_gyroy=0, b_gyroz=0,
			 b_magx=0, b_magy=0, b_magz=0, b_ias=0, b_alt=0, b_temp=0;
	
//GPS Parse Variables
unsigned Lat,Lon;
unsigned int BRG;
float Latitude, Longitude,Bearing; 
unsigned int Lat_Degrees,Lat_Mins,Lon_Degrees,Lon_Mins, Lat_Dir,Lon_Dir, 
			gps_alt,gps_alt_fraction,gps_fix,num_of_sat,gps_time_h=0,gps_time_m=0,gps_time_s=0,gps_speed,gps_speed_fraction,gps_trackangle, 
			gps_trackangle_fraction,gps_date, gps_month, gps_year,True_Track,Mag_Track,GPS_Data,GPS_Month,GPS_Year;
unsigned int Minutes, Lat_Fractions, Lon_Fractions;
unsigned int *GPSBufferAdr=0;



			 
void init_GPS_memory(void) //Bilgisayar 0x00 aldýðýnda hata vermesin diye
{
	int i=0;
	for(i=0;i<128;i++)
		{
			GPS.Buffer1[i]=0x20;	
			GPS.Buffer2[i]=0x20;
			GPS.Buffer2[i]=0x20;
		}	

	*GPS.Buffer1=0x31;
	*GPS.Buffer2=0x32;
	*GPS.Buffer3=0x33;
}	

//*************************HARDWARE INITIAL***************************//
void initIO(void)
{	//Çýkýþ yamak için "0" yaz.
	//TRISE=0x0000; 
	//A9 (gs2),B7(gs1),B6(s/r) are output

	TRISBbits.TRISB6=0; // S/R Signal
	TRISAbits.TRISA9=0; // GS2
	TRISBbits.TRISB7=0; // Gs1

	PORTBbits.RB7=1; //2g, 600mV/g
	PORTAbits.RA9=0;
	PORTBbits.RB6=0; //  s/r initilize	
	
}		


		
void initAdc1(void)//Analog To Digital Converter Initiliaze
{
	AD1CON1bits.FORM   = 0;		// Data Output Format: Integer
	AD1CON1bits.SSRC   = 2;		// Sample Clock Source: GP Timer starts conversion
	AD1CON1bits.ASAM   = 1;		// ADC Sample Control: Sampling begins immediately after conversion
	AD1CON1bits.AD12B  = 1;		// 12-bit ADC operation
	
	AD1CON2bits.CSCNA = 1;		// Scan Input Selections for CH0+ during Sample A bit
	AD1CON2bits.CHPS  = 0;		// Converts CH0
	AD1CON2bits.VCFG = 0;		// AVdd, AVss Ref

	AD1CON3bits.ADRC = 0;		// ADC Clock is derived from Systems Clock
	AD1CON3bits.ADCS = 63;		// ADC Conversion Clock Tad=Tcy*(ADCS+1)= (1/40M)*64 = 1.6us (625Khz)
								// ADC Conversion Time for 10-bit Tc=12*Tab = 19.2us	
	AD1CON1bits.ADDMABM = 0; 	// DMA buffers are built in scatter/gather mode
	AD1CON2bits.SMPI    = (NUM_CHS2SCAN-1);	// 9 ADC Channel is scanned
	AD1CON4bits.DMABL   = 1;	// Each buffer contains 8 words

	//AD1CSSH/AD1CSSL: A/D Input Scan Selection Register
	AD1CSSH = 0x0000;
	
	AD1CSSLbits.CSS0=1;			// Enable AN0 for channel scan //accZ
	AD1CSSLbits.CSS1=1;			// Enable AN1 for channel scan //accY
	AD1CSSLbits.CSS2=1;			// Enable AN2 for channel scan //accX
	AD1CSSLbits.CSS3=1;			// Enable AN3 for channel scan //magZ	
	AD1CSSLbits.CSS4=1;			// Enable AN4 for channel scan //magY
	AD1CSSLbits.CSS5=1;			// Enable AN5 for channel scan //magX
	AD1CSSLbits.CSS8=1;			// Enable AN8 for channel scan //roll (gyroY)
	AD1CSSLbits.CSS9=1;			// Enable AN9 for channel scan //yaw    (gyroZ
	AD1CSSLbits.CSS10=1;		// Enable AN10 for channel scan //pitch (gyroX)
	AD1CSSLbits.CSS12=1;		// Enable AN12 for channel scan //alt   
	AD1CSSLbits.CSS13=1;		// Enable AN13 for channel scan //airs
	AD1CSSLbits.CSS14=1;		// Enable AN14 for channel scan temp
 
 	//AD1PCFGH/AD1PCFGL: Port Configuration Register
	AD1PCFGL=0xFFFF;
	AD1PCFGH=0xFFFF;
	AD1PCFGLbits.PCFG0 = 0;		// AN0 as Analog Input
	AD1PCFGLbits.PCFG1 = 0;		// AN1 as Analog Input 
 	AD1PCFGLbits.PCFG2 = 0;		// AN2 as Analog Input
	AD1PCFGLbits.PCFG3 = 0;		// AN3 as Analog Input 
	AD1PCFGLbits.PCFG4 = 0;		// AN4 as Analog Input
	AD1PCFGLbits.PCFG5 = 0;		// AN5 as Analog Input 
	AD1PCFGLbits.PCFG8 = 0;		// AN8 as Analog Input
	AD1PCFGLbits.PCFG9 = 0;		// AN9 as Analog Input
 	AD1PCFGLbits.PCFG10 = 0;	// AN10 as Analog Input
	AD1PCFGLbits.PCFG12 = 0;	// AN12 as Analog Input
	AD1PCFGLbits.PCFG13 = 0;	// AN13 as Analog Input
	AD1PCFGLbits.PCFG14 = 0;	// AN14 as Analog Input
	
	IFS0bits.AD1IF   = 0;		// Clear the A/D interrupt flag bit
	IEC0bits.AD1IE   = 0;		// Do Not Enable A/D interrupt 
	AD1CON1bits.ADON = 1;		// Turn on the A/D converter
}



void initTmr3() //ADC Sample Time
{
	T3CONbits.TON = 0;		// Disable Time
	T3CONbits.TCKPS=0; 		//Prescale 0:0  1 tick=25,24376 nanoseconds
	T3CONbits.TCS=0;  		//Internal clock (FCY=0)
	TMR3 = 0x0000;
	//Load the period value
	//PR3=100;				// Trigger ADC1 every 25,24 usec
	//PR3=1000;				// Trigger ADC1 every 25,24 usec	
	//PR3=2000;				// Trigger ADC1 every 50,48 usec
	PR3  = 8000;			// Trigger ADC1 every 202 usec
	//PR3  = 65500;			// Trigger ADC1 every 1,653 msec
	IFS0bits.T3IF = 0;		// Clear Timer 3 interrupt
	IEC0bits.T3IE = 0;		// Disable Timer 3 interrupt
	T3CONbits.TON = 1;		//Start Timer 3
}



void initTmr1() 		//IMU Data Output Frequency
{	
	T1CONbits.TON = 0; 		// Disable Timer
	T1CONbits.TCS = 0; 		// Select internal instruction cycle clock
	T1CONbits.TGATE = 0; 	// Disable Gated Timer mode
	T1CONbits.TCKPS = 0b11; // Select 1:256 Prescaler  (1 tick=6,4624 microseconds)
	TMR1 = 0x00; 			// Clear timer register
	// Load the period value
//	PR1=1547;				// 100 Hz
	PR1=3094;				// 50 Hz
//	PR1=6188;				// 25 Hz
	//PR1=9260;
	IPC0bits.T1IP = 0x01; 	// Set Timer1 Interrupt Priority Level
	IFS0bits.T1IF = 0; 		// Clear Timer1 Interrupt Flag
	IEC0bits.T1IE = 1; 		// Enable Timer1 interrupt
	T1CONbits.TON = 1; 		// Start Timer
}


void initTmr2() //s/r Time
{
	T2CONbits.TON = 0;		// Disable Time
	//T2CONbits.TCKPS=0b11; // Select 1:256 Prescaler  (1 tick=6,4624 microseconds)
	T2CONbits.TCKPS=0b10; // Select 1:64 Prescaler  (1 tick=1,6 microseconds)
	T2CONbits.TCS=0;  		//Internal clock (FCY=0)
	T2CONbits.TGATE = 0; 	// Disable Gated Timer mode
	TMR2 = 0x00;
	//Load the period value
	PR2  = 65535;			// Trigger ADC1 every 0,4235 sec
	IPC1bits.T2IP = 0x01; 	// Set Timer1 Interrupt Priority Level
	IFS0bits.T2IF = 0;		// Clear Timer 2 interrupt
	IEC0bits.T2IE = 1;		// Enable Timer 2 interrupt
	T2CONbits.TON = 1;		//Start Timer 2
}

//MPC555 IMU Data Output
void cfgUart2(void) 	//Config UART2 For IMU Data Output
{
	U2MODEbits.STSEL = 0;		// 1-stop bit
	U2MODEbits.PDSEL = 0;		// No Parity, 8-data bits
	U2MODEbits.ABAUD = 0;		// Autobaud Disabled
	U2MODEbits.BRGH = 0;		// Bit3 16 clocks per bit period
	U2BRG = 21;					// 40Mhz (36,85Mhz) osc, 115200 Baud
								//NOT: BRGH=1 ve U2BRG=85 icin calismadi.
	U2STAbits.UTXISEL0 = 0;		// Interrupt after one Tx character is transmitted
	U2STAbits.UTXISEL1 = 0;			                            
	U2STAbits.URXISEL  = 0;		// Interrupt after one RX character is received

	U2MODEbits.UARTEN   = 1;	// Enable UART
	U2STAbits.UTXEN     = 1;	// Enable UART Tx
	
	IFS4bits.U2EIF = 0; 		// Clear UART2 error interrupt Flag
	IEC4bits.U2EIE = 1; 		// Enable UART2 error interrupt
}


void cfgUart1(void) 	//Config UART1 For GPS Data Input (INTERRUPT)
{
	U1MODEbits.STSEL = 0;			// 1-stop bit
	U1MODEbits.PDSEL = 0;			// No Parity, 8-data bits
	U1MODEbits.ABAUD = 0;			// Autobaud Disabled
	U1MODEbits.BRGH = 0;			// Bit3 16 clocks per bit period
	U1BRG = 42;						// 40Mhz (36,85Mhz) osc, 115200 Baud
									//NOT: BRGH=1 ve U1BRG=85 icin calismadi.
	//  Configure UART for DMA transfers
	U1STAbits.UTXISEL0 = 0;			// Interrupt after one Tx character is transmitted
	U1STAbits.UTXISEL1 = 0;			                            
	U1STAbits.URXISEL  = 0;			// Interrupt after one RX character is received
	//********************************************************************************/
	//IPC7 = 0x4400; 	// Mid Range Interrupt Priority level (4) 7:Highest, 0:Lowest, no urgent reason
	
	IFS0bits.U1TXIF = 0;	// Clear the Transmit Interrupt Flag
	IEC0bits.U1TXIE = 0;	// Enable Transmit Interrupts
	IFS0bits.U1RXIF = 0;	// Clear the Receive Interrupt Flag
	IEC0bits.U1RXIE = 1;	// Enable Recieve Interrupts				

	U1MODEbits.UARTEN   = 1;		// Enable UART
	U1STAbits.UTXEN     = 0;		// Enable UART Tx
	//IEC4bits.U1EIE = 0;
	IFS4bits.U1EIF = 0; // Clear UART1 error interrupt Flag
	IEC4bits.U1EIE = 1; // Enable UART1 error interrupt
}




unsigned int adc_ch=0;

//**************************FUNCTIONS********************************//

unsigned int sum[12], ch_no[]={0,1,2,3,4,5,8,9,10,12,13,14};

unsigned int Faccx=0,Faccy=0,Faccz=0,Fgyrox=0,Fgyroy=0, Fgyroz=0,Fmagx=0,Fmagy=0,Fmagz=0, Ftemp=0,Fias=0,Falt=0;

void ProcessADCSamplesA()	//void ProcessADCSamples(int * AdcBuffer)
{
	sum[0]=0;sum[1]=0;sum[2]=0;sum[3]=0;sum[4]=0;sum[5]=0;
	sum[6]=0;sum[7]=0;sum[8]=0; sum[9]=0; sum[10]=0; sum[11]=0;
	
	unsigned int i=0,j=0;
	for(i=0;i<12;i++)
		for(j=0;j<SAMP_BUFF_SIZE;j++) sum[i]=sum[i]+ADCBufferA[ch_no[i]][j];

	
	Faccx=((sum[2]/SAMP_BUFF_SIZE)+accx)/2;
	Faccy=((sum[1]/SAMP_BUFF_SIZE)+accy)/2;
	Faccz=((sum[0]/SAMP_BUFF_SIZE)+accz)/2;
	
	Fgyrox=((sum[8]/SAMP_BUFF_SIZE)+gyrox)/2;
	Fgyroy=((sum[6]/SAMP_BUFF_SIZE)+gyroy)/2;
	Fgyroz=((sum[7]/SAMP_BUFF_SIZE)+gyroz)/2;
	
	Fmagx=((sum[5]/SAMP_BUFF_SIZE)+magx)/2;
	Fmagy=((sum[4]/SAMP_BUFF_SIZE)+magy)/2;
	Fmagz=((sum[3]/SAMP_BUFF_SIZE)+magz)/2;
	
	Ftemp=((sum[11]/SAMP_BUFF_SIZE)+temp)/2;
	Falt=((sum[9]/SAMP_BUFF_SIZE)+alt)/2;
	Fias=((sum[10]/SAMP_BUFF_SIZE)+ias)/2;
	

	*(TXBuffer.ADCSection)=0xFAFF;
	*(TXBuffer.ADCSection+1)=(unsigned int)(Faccx); //accx  sum2
	*(TXBuffer.ADCSection+2)=(unsigned int)(Faccy); //accy  sum1
	*(TXBuffer.ADCSection+3)=(unsigned int)(Faccz); //accz  sum0
	*(TXBuffer.ADCSection+4)=(unsigned int)(Fgyrox); //gyrox (pitch)  sum8
	*(TXBuffer.ADCSection+5)=(unsigned int)(Fgyroy) ; //gyroy sum6
	*(TXBuffer.ADCSection+6)=(unsigned int)(Fgyroz); //gyroZ  sum7
	*(TXBuffer.ADCSection+7)=(unsigned int)(Fmagx ); //magX  sum5
	*(TXBuffer.ADCSection+8)=(unsigned int)(Fmagy); //magY  sum4
	*(TXBuffer.ADCSection+9)=(unsigned int)(Fmagz); //magZ  sum3
	*(TXBuffer.ADCSection+10)=(unsigned int)(Ftemp); //temp sum11
	*(TXBuffer.ADCSection+11)=(unsigned int)(Falt); //alt  sum9
	*(TXBuffer.ADCSection+12)=(unsigned int)(Fias); //ias  sum10
	*(TXBuffer.ADCSection+13)=0x00; //ias
	*(TXBuffer.ADCSection+14)=0x00; //ias
	*(TXBuffer.ADCSection+15)=0x00; //ias
	*(TXBuffer.ADCSection+16)=0x00; //ias
	*(TXBuffer.ADCSection+17)=0x00; //ias
	*(TXBuffer.ADCSection+18)=0x00; //ias
	*(TXBuffer.ADCSection+19)=0x00; //ias
	*(TXBuffer.ADCSection+20)=0x00; //ias
	*(TXBuffer.ADCSection+21)=0x0A0D;	
//	*(TXBuffer.ADCSection+21)=0x0A0A;	
	
	accx=Faccx;
	accy=Faccy;
	accz=Faccz;
	
	gyrox=Fgyrox;
	gyroy=Fgyroy;
	gyroz=Fgyroz;
	
	magx=Fmagx;
	magy=Fmagy;
	magz=Fmagz;
	
	temp=Ftemp;
	alt=Falt;
	ias=Fias;
	
}
void ProcessADCSamplesB()
{
	sum[0]=0;sum[1]=0;sum[2]=0;sum[3]=0;sum[4]=0;sum[5]=0;
	sum[6]=0;sum[7]=0;sum[8]=0; sum[9]=0; sum[10]=0; sum[11]=0;
	
	unsigned int i=0,j=0;
	for(i=0;i<12;i++)
		for(j=0;j<SAMP_BUFF_SIZE;j++) sum[i]=sum[i]+ADCBufferB[ch_no[i]][j];
	
		Faccx=((sum[2]/SAMP_BUFF_SIZE)+accx)/2;
	Faccy=((sum[1]/SAMP_BUFF_SIZE)+accy)/2;
	Faccz=((sum[0]/SAMP_BUFF_SIZE)+accz)/2;
	
	Fgyrox=((sum[8]/SAMP_BUFF_SIZE)+gyrox)/2;
	Fgyroy=((sum[6]/SAMP_BUFF_SIZE)+gyroy)/2;
	Fgyroz=((sum[7]/SAMP_BUFF_SIZE)+gyroz)/2;
	
	Fmagx=((sum[5]/SAMP_BUFF_SIZE)+magx)/2;
	Fmagy=((sum[4]/SAMP_BUFF_SIZE)+magy)/2;
	Fmagz=((sum[3]/SAMP_BUFF_SIZE)+magz)/2;
	
	Ftemp=((sum[11]/SAMP_BUFF_SIZE)+temp)/2;
	Falt=((sum[9]/SAMP_BUFF_SIZE)+alt)/2;
	Fias=((sum[10]/SAMP_BUFF_SIZE)+ias)/2;
	

	*(TXBuffer.ADCSection)=0xFAFF;
	*(TXBuffer.ADCSection+1)=(unsigned int)(Faccx); //accx  sum2
	*(TXBuffer.ADCSection+2)=(unsigned int)(Faccy); //accy  sum1
	*(TXBuffer.ADCSection+3)=(unsigned int)(Faccz); //accz  sum0
	*(TXBuffer.ADCSection+4)=(unsigned int)(Fgyrox); //gyrox (pitch)  sum8
	*(TXBuffer.ADCSection+5)=(unsigned int)(Fgyroy); //gyroy sum6
	*(TXBuffer.ADCSection+6)=(unsigned int)(Fgyroz); //gyroZ  sum7
	*(TXBuffer.ADCSection+7)=(unsigned int)(Fmagx ); //magX  sum5
	*(TXBuffer.ADCSection+8)=(unsigned int)(Fmagy); //magY  sum4
	*(TXBuffer.ADCSection+9)=(unsigned int)(Fmagz); //magZ  sum3
	*(TXBuffer.ADCSection+10)=(unsigned int)(Ftemp); //temp sum11
	*(TXBuffer.ADCSection+11)=(unsigned int)(Falt); //alt  sum9
	*(TXBuffer.ADCSection+12)=(unsigned int)(Fias); //ias  sum10
	*(TXBuffer.ADCSection+13)=0x00; //ias
	*(TXBuffer.ADCSection+14)=0x00; //ias
	*(TXBuffer.ADCSection+15)=0x00; //ias
	*(TXBuffer.ADCSection+16)=0x00; //ias
	*(TXBuffer.ADCSection+17)=0x00; //ias
	*(TXBuffer.ADCSection+18)=0x00; //ias
	*(TXBuffer.ADCSection+19)=0x00; //ias
	*(TXBuffer.ADCSection+20)=0x00; //ias
	*(TXBuffer.ADCSection+21)=0x0A0D;	
//	*(TXBuffer.ADCSection+21)=0x0A0A;	
	
	accx=Faccx;
	accy=Faccy;
	accz=Faccz;
	
	gyrox=Fgyrox;
	gyroy=Fgyroy;
	gyroz=Fgyroz;
	
	magx=Fmagx;
	magy=Fmagy;
	magz=Fmagz;
	
	temp=Ftemp;
	alt=Falt;
	ias=Fias;
	

}


void cfgDma0Uart2Tx(unsigned int tx_byte_size) // MPC555 için DMA0 configuration for IMU Data Output(TX)
{
	//  Associate DMA Channel 0 with UART Tx
	DMA0REQ = 0x001F;					// Select UART2 Transmitter
	DMA0PAD = (volatile unsigned int) &U2TXREG;
	
	DMA0CONbits.AMODE = 0;
	//DMA0CONbits.MODE  = 1;
	//11 = One-Shot, Ping-Pong modes enabled (one block transfer from/to each DMA RAM buffer)
	DMA0CONbits.MODE  = 3; //One-Shot, Ping-Pong Mode
	//DMA0CONbits.MODE  = 1; // One-Shot, Ping-Pong modes disabled
	DMA0CONbits.DIR   = 1;
	DMA0CONbits.SIZE  = 1;	//Send Byte. Because TX Buffer is 8 bit(if 9 bit conf. is not used)
	DMA0CNT = tx_byte_size;						// 28 DMA requests

	DMA0STA = __builtin_dmaoffset(TXBuffer.ADCSection);
	DMA0STB = __builtin_dmaoffset(TXBuffer.GPSSection);
//	DMA0STB = __builtin_dmaoffset(TXBuffer.ADCSection);

	//	Enable DMA Interrupts
	
	IFS0bits.DMA0IF  = 0;			// Clear DMA Interrupt Flag
	IEC0bits.DMA0IE  = 1;			// Enable DMA interrupt

}

void cfgDma1Uart2Rx(void) // MPC555 DMA1 configuration for UARt2 RX (NOT USED)
{
	//********************************************************************************
	//  Associate DMA Channel 1 with UART1 Rx
	//********************************************************************************/
	DMA1REQ = 0x001E;					// Select UART2 Receiver
	DMA1PAD = (volatile unsigned int) &U2RXREG;

	//********************************************************************************
	//  STEP 4:
	//  Configure DMA Channel 1 to:
	//  Transfer data from UART to RAM Continuously
	//  Register Indirect with Post-Increment
	//  Using two ‘ping-pong’ buffers
	//  8 transfers per buffer
	//  Transfer words
	//********************************************************************************/
	//DMA1CON = 0x0002;					// Continuous, Ping-Pong, Post-Inc, Periph-RAM
	DMA1CONbits.AMODE = 0;
	DMA1CONbits.MODE  = 2;
	DMA1CONbits.DIR   = 0;
	DMA1CONbits.SIZE  = 0;
	DMA1CNT = 7;						// 8 DMA requests

	//********************************************************************************
	//  STEP 6:
	//  Associate two buffers with Channel 1 for ‘Ping-Pong’ operation
	//********************************************************************************/
	DMA1STA = __builtin_dmaoffset(RX_BufferA);
	DMA1STB = __builtin_dmaoffset(RX_BufferB);

	//********************************************************************************
	//  STEP 8:
	//	Enable DMA Interrupts
	//********************************************************************************/
	IFS0bits.DMA1IF  = 0;			// Clear DMA interrupt
	IEC0bits.DMA1IE  = 1;			// Enable DMA interrupt

	//********************************************************************************
	//  STEP 9:
	//  Enable DMA Channel 1 to receive UART data
	//********************************************************************************/
	DMA1CONbits.CHEN = 1;			// Enable DMA Channel
		
}

void initDma2(void)  //Read ADC Data To the ADCBufferA and B
{
	DMA2CONbits.AMODE = 2;			// Configure DMA for Peripheral indirect mode
	DMA2CONbits.MODE  = 2;			// Configure DMA for Continuous Ping-Pong mode
	DMA2PAD=(int)&ADC1BUF0;
	DMA2CNT = (SAMP_BUFF_SIZE*NUM_CHS2SCAN)-1;					
	DMA2REQ = 13;					// Select ADC1 as DMA Request source

	DMA2STA = __builtin_dmaoffset(ADCBufferA);		
	DMA2STB = __builtin_dmaoffset(ADCBufferB);

	IFS1bits.DMA2IF = 0;			//Clear the DMA interrupt flag bit
    IEC1bits.DMA2IE = 1;			//Set the DMA interrupt enable bit

	DMA2CONbits.CHEN=1;				// Enable DMA

}

void cfgDma3UartRx(void) // DMA3 configuration for writing GPS Data To The Memory v1
{
	//********************************************************************************
	//  Associate DMA Channel 3 with UART1 Rx(GPS Data)
	//********************************************************************************/
	DMA3REQ = 0x000B;					// Select UART1 Receiver. 0001011 = UART1RX 
	DMA3PAD = (volatile unsigned int) &U1RXREG;

	DMA3CONbits.AMODE = 0;
	//DMA3CONbits.MODE  = 2;
	DMA3CONbits.MODE  = 3;				//01 = One-Shot, Ping-Pong modes disabled
	DMA3CONbits.DIR   = 0;
	DMA3CONbits.SIZE  = 1;
	DMA3CNT = 255;						// 256 DMA requests

	DMA3STA = __builtin_dmaoffset(GPS.Buffer1);
	

	//********************************************************************************
	//	Enable DMA Interrupts and DMA Channel
	//********************************************************************************/
	IFS2bits.DMA3IF  = 0;			// Clear DMA interrupt
	IEC2bits.DMA3IE  = 0;			// Enable DMA interrupt

	DMA3CONbits.CHEN = 0;			// Enable DMA Channel

}

unsigned int buf_size=1;
unsigned int  sent=0;

void __attribute__((interrupt, no_auto_psv)) _DMA0Interrupt(void) //IMU Data Output(TX)
{
	static unsigned int BufferCount = 0;

	
	if(BufferCount == 1) //Now sent ADC Data
	{
	//	DMA0CNT =IMU_TX_DATA_BYTE_SIZE;
		DMA0STA = __builtin_dmaoffset(TXBuffer.ADCSection);	
	}
	
	else 	 //Now sent GPS Data
	{
		//To send GPSSection
		DMA0STB = __builtin_dmaoffset(TXBuffer.GPSSection);	

	 }

	BufferCount ^= 1;	
	IFS0bits.DMA0IF = 0;			// Clear the DMA0 Interrupt Flag;

}
void __attribute__((interrupt, no_auto_psv)) _DMA1Interrupt(void)//DMA1 configuration for UART2 RX (NOT USED)
{
	static unsigned int BufferCount = 0;  // Keep record of which buffer contains Rx Data
	
	DMA0CONbits.CHEN  = 1;			// Re-enable DMA0 Channel
	DMA0REQbits.FORCE = 1;			// Manual mode: Kick-start the first transfer

	BufferCount ^= 1;				
	IFS0bits.DMA1IF = 0;			// Clear the DMA1 Interrupt Flag
}


unsigned int DmaBuffer = 0;


void __attribute__((interrupt, no_auto_psv)) _DMA2Interrupt(void) //Read ADC Data To the ADCBufferA and B
{
	if(DmaBuffer == 0)
	{
		ProcessADCSamplesA();
	}
	else
	{
		ProcessADCSamplesB();
	}
	DmaBuffer ^= 1;
	IFS1bits.DMA2IF = 0;		// Clear the DMA2 Interrupt Flag
}

void __attribute__((interrupt, no_auto_psv)) _DMA3Interrupt(void)//GPS Data To GPS BufferA-B
{
	static unsigned int BufferCount = 0;  // Keep record of which buffer contains Rx Data
	BufferCount ^= 1;				
	IFS2bits.DMA3IF = 0;			// Clear the DMA3 Interrupt Flag
}

void __attribute__ ((interrupt, no_auto_psv)) _U1ErrInterrupt(void)
{
	IFS4bits.U1EIF = 0; // Clear the UART1 Error Interrupt Flag
}

void __attribute__ ((interrupt, no_auto_psv)) _U2ErrInterrupt(void)
{
	IFS4bits.U2EIF = 0; // Clear the UART1 Error Interrupt Flag
	
}


void __attribute__((__interrupt__, no_auto_psv)) _T1Interrupt(void)
{
  	//Send Data to the UART from DMA Buffer

 	DMA0CONbits.CHEN  = 1;			// Re-enable DMA0 Channel
	DMA0REQbits.FORCE = 1;			// Manual mode: Kick-start the first transfer
	
	IFS0bits.T1IF = 0; // Clear Timer1 Interrupt Flag
}


unsigned int update_ok=0, GPSi=1, GPS_Sync=0, GPS_Buffer_Num=0;
char RXData=0;


void __attribute__ ((interrupt, no_auto_psv)) _U1RXInterrupt (void)/*GPS Data Sync*/
{		
	if(GPS_Buffer_Num==0){
		*(GPS.Buffer1+GPSi)=(char)U1RXREG;
		GPSi++;
		if(*(GPS.Buffer1+GPSi-1)==0x24) {GPSi=1; GPS_Sync=1;}
	
		if(*(GPS.Buffer1+GPSi-1)==0x0A && GPS_Sync==1) 
		{
			GPSi=1, GPS_Buffer_Num++;GPS_Sync==0;
			//parse_gps_data();
		}
	}
	
	if(GPS_Buffer_Num==1){
		*(GPS.Buffer2+GPSi)=(char)U1RXREG;
		GPSi++;
		if(*(GPS.Buffer2+GPSi-1)==0x24) {GPSi=1; GPS_Sync=1; }
	
		if(*(GPS.Buffer2+GPSi-1)==0x0A && GPS_Sync==1) 
		{
			GPSi=1, GPS_Buffer_Num++; GPS_Sync==0;
			//parse_gps_data();

		}
	}
	
	if(GPS_Buffer_Num==2){
		*(GPS.Buffer3+GPSi)=(char)U1RXREG;
		GPSi++;
		if(*(GPS.Buffer3+GPSi-1)==0x24) {GPSi=1; GPS_Sync=1;}
	
		if(*(GPS.Buffer3+GPSi-1)==0x0A && GPS_Sync==1) 
		{
			GPSi=1, GPS_Buffer_Num=0;GPS_Sync==0;
			parse_gps_data();
		}
	}
	
	IFS0bits.U1RXIF = 0;
	
}

char gps_delim=',';
char *gps_gga, *gps_rmc, *gps_vtg;
int virgul_index[14];
char temp_string[6], temp_string2[6];
 int	point_place=0;
	int v_i=0, t_i=0;
	
	int gps_gga_available=0, gps_rmc_available=0, gps_vtg_available=0;
	


void parse_gps_data(void)
{
	//////////////////
	
	if(*(GPS.Buffer1+3)=='G' && *(GPS.Buffer1+4)=='G' && *(GPS.Buffer1+5)=='A')
	{
		gps_gga=GPS.Buffer1;
		gps_gga_available=1;
	}
	
	if(*(GPS.Buffer2+3)=='G' && *(GPS.Buffer2+4)=='G' && *(GPS.Buffer2+5)=='A')
	{
		gps_gga=GPS.Buffer2;
		gps_gga_available=1;
	}	
	
	if(*(GPS.Buffer3+3)=='G' && *(GPS.Buffer3+4)=='G' && *(GPS.Buffer3+5)=='A')
	{
		gps_gga=GPS.Buffer3;
		gps_gga_available=1;
	}
	
	
	if(*(GPS.Buffer1+3)=='R' && *(GPS.Buffer1+4)=='M' && *(GPS.Buffer1+5)=='C')
	{
		gps_rmc=GPS.Buffer1;
		gps_rmc_available=1;
	}
	
	if(*(GPS.Buffer2+3)=='R' && *(GPS.Buffer2+4)=='M' && *(GPS.Buffer2+5)=='C')
	{
		gps_rmc=GPS.Buffer2;
		gps_rmc_available=1;
	}	
	
	if(*(GPS.Buffer3+3)=='R' && *(GPS.Buffer3+4)=='M' && *(GPS.Buffer3+5)=='C')
	{
		gps_rmc=GPS.Buffer3;
		gps_rmc_available=1;
	}
	
	
		if(*(GPS.Buffer1+3)=='V' && *(GPS.Buffer1+4)=='T' && *(GPS.Buffer1+5)=='G')
	{
		gps_vtg=GPS.Buffer1;
		gps_vtg_available=1;
	}
	
	if(*(GPS.Buffer2+3)=='V' && *(GPS.Buffer2+4)=='T' && *(GPS.Buffer2+5)=='G')
	{
		gps_vtg=GPS.Buffer2;
		gps_vtg_available=1;
	}	
	
	if(*(GPS.Buffer3+3)=='V' && *(GPS.Buffer3+4)=='T' && *(GPS.Buffer3+5)=='G')
	{
		gps_vtg=GPS.Buffer3;
		gps_vtg_available=1;
	}
	
	int i=0, j=0;

	if( gps_gga_available==1)
	{		
	
		while	 ( !(*(gps_gga+i)=='*'))
		{ 
		     if( *(gps_gga+i)==',')
		       { 
			     virgul_index[j]=i; j++;
			   }
			  i++;
			} 
						
		   
		   gps_time_h= *(gps_gga+(virgul_index[0]+2))&mask;  //8
		   gps_time_h+= (unsigned int)(*(gps_gga+(virgul_index[0]+1))&mask)*10;  //7
		   
		   gps_time_m= *(gps_gga+(virgul_index[0]+4))&mask;  //10
		   gps_time_m+= (unsigned int)(*(gps_gga+(virgul_index[0]+3))&mask)*10;  //9
		   
		   gps_time_s= *(gps_gga+(virgul_index[0]+6))&mask;  //12
		   gps_time_s+= (unsigned int)(*(gps_gga+(virgul_index[0]+5))&mask)*10;  //11
		   
		  
		        
		     Lat_Degrees = *(gps_gga+(virgul_index[1]+2))&mask;  //19
			 Lat_Degrees += (unsigned int)(*(gps_gga+(virgul_index[1]+1))&mask)*10;  //18
					  
			 Lat_Mins = *(gps_gga+(virgul_index[1]+4))&mask;  //21
			 Lat_Mins += (unsigned int)(*(gps_gga+(virgul_index[1]+3))&mask)*10; //20
			 
			 Lat_Fractions = *(gps_gga+(virgul_index[1]+9))&mask;  //26
			 Lat_Fractions +=(*(gps_gga+(virgul_index[1]+8))&mask)*10; //25
			 Lat_Fractions +=(unsigned int)(*(gps_gga+(virgul_index[1]+7))&mask)*100;  //24
			 Lat_Fractions +=(unsigned int)(*(gps_gga+(virgul_index[1]+6))&mask)*1000;  //23
		
			 Lon_Degrees = *(gps_gga+(virgul_index[3]+3))&mask;          // 32
			 Lon_Degrees +=(unsigned int)(*(gps_gga+(virgul_index[3]+2))&mask)*10;  //31
			 Lon_Degrees +=(unsigned int)(*(gps_gga+(virgul_index[3]+1))&mask)*100;  //30
			 
			 Lon_Mins = *(gps_gga+(virgul_index[3]+5))&mask; //34
			 Lon_Mins +=(unsigned int)(*(gps_gga+(virgul_index[3]+4))&mask)*10; //33
			 
			 Lon_Fractions = *(gps_gga+(virgul_index[3]+10))&mask; //39
			 Lon_Fractions +=(*(gps_gga+(virgul_index[3]+9))&mask)*10; //38
			 Lon_Fractions +=(unsigned int)(*(gps_gga+(virgul_index[3]+8))&mask)*100; //37
			 Lon_Fractions +=(unsigned int)(*(gps_gga+(virgul_index[3]+7))&mask)*1000; //36
			 
			 Lat_Dir=  *(gps_gga+(virgul_index[2]+1)); //28
			 Lon_Dir=  *(gps_gga+(virgul_index[4]+1)); //41
			 
			point_place=0;
			v_i=0, t_i=0;
			for(v_i=(virgul_index[8]+1);v_i<virgul_index[9];v_i++)
		 	{ 
			 	temp_string[t_i]= *(gps_gga+v_i);
			 	if( temp_string[t_i]=='.') point_place=t_i;
		  	 	t_i++;	 
		 	}
		 
		 
		 	if(point_place==4)
		 	{
			 	gps_alt = *(gps_gga+(virgul_index[8]+4))&mask;          // 
			 	gps_alt +=(unsigned int)(*(gps_gga+(virgul_index[8]+3))&mask)*10;  //
			    gps_alt +=(unsigned int)(*(gps_gga+(virgul_index[8]+2))&mask)*100;  //
			    gps_alt +=(unsigned int)(*(gps_gga+(virgul_index[8]+1))&mask)*1000;  //
			 	
			 	gps_alt_fraction = (unsigned int)*(gps_gga+(virgul_index[8]+point_place+2))&mask;          // 
			 }
		 
		 	if(point_place==3)
		 	{
			 	gps_alt = *(gps_gga+(virgul_index[8]+3))&mask;          // 
			 	gps_alt +=(unsigned int)(*(gps_gga+(virgul_index[8]+2))&mask)*10;  //
			    gps_alt +=(unsigned int)(*(gps_gga+(virgul_index[8]+1))&mask)*100;  //
			 	
			 	gps_alt_fraction = (unsigned int)*(gps_gga+(virgul_index[8]+point_place+2))&mask;          // 
			 }
			 
			else if(point_place==2)
		 	{
			 	gps_alt = *(gps_gga+(virgul_index[8]+2))&mask;          // 
			 	gps_alt +=(unsigned int)(*(gps_gga+(virgul_index[8]+1))&mask)*10;  //
			 	gps_alt_fraction = (unsigned int)*(gps_gga+(virgul_index[8]+point_place+2))&mask;          // 
			 }
			 
			else if(point_place==1)
		 	{
			 	gps_alt =(unsigned int) *(gps_gga+(virgul_index[8]+1))&mask;          // 
			  	gps_alt_fraction = (unsigned int)*(gps_gga+(virgul_index[8]+point_place+2))&mask;          // 
			 }
			 
			gps_fix=(unsigned int)(*(gps_gga+(virgul_index[5]+1))); //43
			num_of_sat=(unsigned int)(*(gps_gga+(virgul_index[6]+1)))&mask;; //43	
			if((*(gps_gga+(virgul_index[6]+2)))!=',') num_of_sat +=(unsigned int)((*(gps_gga+(virgul_index[6]+1)))&mask)*10; //43	
			 
			 
			 gps_gga_available=0;
	}
	
	
	if(gps_rmc_available==1)
	{	
		///////////////////////////////////////////////////////////////////////////////////
		/// Parse GPRMC Data
		i=0, j=0;
		while	 ( !(*(gps_rmc+i)=='*'))
		{ 
		     if( *(gps_rmc+i)==',')
		       { 
			     virgul_index[j]=i; j++;
			   }
			  i++;
		} 
		
		//Ground Speed
		point_place=0;
		v_i=0, t_i=0;
		for(v_i=(virgul_index[6]+1);v_i<virgul_index[7];v_i++)
		 { 
			 temp_string[t_i]= *(gps_rmc+v_i);
			 if( temp_string[t_i]=='.') point_place=t_i+1;
		  	 t_i++;	 
		 }
		gps_speed=(unsigned int)strtoul(&temp_string[0], '.',0);
	   // gps_speed_fraction=(unsigned int)strtoul(&temp_string[point_place], &temp_string[t_i],0);
		
		point_place=0;
		v_i=0, t_i=0;
		for(v_i=(virgul_index[7]+1);v_i<virgul_index[8];v_i++)
		 { 
			 temp_string2[t_i]= *(gps_rmc+v_i);
			 if( temp_string2[t_i]=='.') point_place=t_i+1;
		  	 t_i++;	 
		 }
		 t_i--;
		gps_trackangle=(unsigned int)strtoul(&temp_string2[0], '.',0);
	   // gps_trackangle_fraction=(unsigned int)strtoul(&temp_string2[point_place],&temp_string2[t_i],0);
		
		
		gps_date= *(gps_rmc+(virgul_index[8]+2))&mask;  //58
		gps_date += (unsigned int)(*(gps_rmc+(virgul_index[8]+1))&mask)*10;  //57
					  
		gps_month= *(gps_rmc+(virgul_index[8]+4))&mask;  //60
		gps_month += (unsigned int)(*(gps_rmc+(virgul_index[8]+3))&mask)*10;  //59
		
		gps_year= *(gps_rmc+(virgul_index[8]+6))&mask;  //62
		gps_year += (unsigned int)(*(gps_rmc+(virgul_index[8]+5))&mask)*10;  //61
		
		gps_rmc_available=0;
	}
	
		*(TXBuffer.GPSSection)=0xFBFF;
		*(TXBuffer.GPSSection+1)=Lat_Degrees;
		*(TXBuffer.GPSSection+2)=Lat_Mins;
		*(TXBuffer.GPSSection+3)=Lat_Fractions;
		*(TXBuffer.GPSSection+4)=Lat_Dir;
		*(TXBuffer.GPSSection+5)=Lon_Degrees;
		*(TXBuffer.GPSSection+6)=Lon_Mins;
		*(TXBuffer.GPSSection+7)=Lon_Fractions;
		*(TXBuffer.GPSSection+8)=Lon_Dir;
		*(TXBuffer.GPSSection+9)=gps_alt;
		*(TXBuffer.GPSSection+10)=gps_alt_fraction;
		*(TXBuffer.GPSSection+11)=gps_speed;
		*(TXBuffer.GPSSection+12)=gps_speed_fraction;
		*(TXBuffer.GPSSection+13)=gps_trackangle;
		*(TXBuffer.GPSSection+14)=gps_trackangle_fraction;
		*(TXBuffer.GPSSection+15)=gps_date;
		*(TXBuffer.GPSSection+16)=gps_month;
		*(TXBuffer.GPSSection+17)=gps_year;
		*(TXBuffer.GPSSection+18)=gps_time_h;
		*(TXBuffer.GPSSection+19)=gps_time_m;
		*(TXBuffer.GPSSection+20)=gps_time_s;
		*(TXBuffer.GPSSection+21)=0x0A0D;
		
}



int timer_tick=0;
static unsigned int TimerCount = 0;
void __attribute__((__interrupt__, no_auto_psv)) _T2Interrupt(void)
{
	//PORTDbits.RD1=1;
	
	T2CONbits.TON = 0;
	if(TimerCount==0)
	{
		TMR2 = 0x0000;
		PORTBbits.RB6=1;
		//PORTDbits.RD1=1;
		PR2  = 1;			// Trigger ADC1 every 1,6 usec		
	}
	
	if(TimerCount==1)
	{
			TMR2 = 0x0000;
		PORTBbits.RB6=0;
		//PORTDbits.RD1=0;
		PR2  = 65535;	// Trigger ADC1 every 0,4235 sec		
		
	}

	TimerCount ^=1;
	IFS0bits.T2IF = 0; // Clear Timer1 Interrupt Flag
	IEC0bits.T2IE = 1;		// Enable Timer 2 interrupt
	T2CONbits.TON = 1;
}



