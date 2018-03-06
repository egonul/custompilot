/************************************************************************************
Ersin GONUL 
Inertial Measurement Unit
10.10.2011 
FRC_PLL= 79.2275Mhz
************************************************************************************/
#include "p33FJ256GP710a.h"
#define  IMU_TX_DATA_BYTE_SIZE 43

_FICD( ICS_PGD2);					//Choose PGD2 for debug
_FOSCSEL(FNOSC_FRCPLL)              //set clock for internal OSC with PLL
_FOSC(OSCIOFNC_OFF & POSCMD_NONE)   //no clock output, external OSC disabled

_FWDT(FWDTEN_OFF); 						            // Watchdog Timer Enabled/disabled by user software

unsigned int sensor_read=65000;													

int main(void)
{
	/***** Setup internal clock for 79.227500MHz
 	   7.37/2=3.685*43=158.455/2=79.2275  ******/
  	CLKDIVbits.PLLPRE=0;        // PLLPRE (N2) 0=/2
 	PLLFBD=41;                  // PLL Multiplier (M) = +2
  	CLKDIVbits.PLLPOST=0;       // PLLPOST (N1) 0=/2
  	//Fcy=Fosc/2=79.227500Mhz/2=39.61375Mhz
    while(!OSCCONbits.LOCK);    // wait for PLL ready
	
	initIO();
	initAdc1();             	// Initialize the A/D converter to convert IMU Data
	initDma2();					// Initialise the DMA controller to buffer ADC data in conversion order
	initTmr1();					// IMU message rate(frequency)	
	initTmr2();					// Timer for s/r Time
	initTmr3();					// ADC Sample Time
	init_GPS_memory();			//Erase GP MEmory Block		
	cfgUart1(); 				// GPS Communication
	cfgUart2(); 				//IMU Data Output to MPC555/PC
	cfgDma0Uart2Tx(IMU_TX_DATA_BYTE_SIZE);	// Configure DMAchannel 0 for transmission (TX).
	cfgDma3UartRx();
	
	while(1)
    {    
	    
	} 
}

