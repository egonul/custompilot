
%Discrete Time Intervals
%All step sizes shall be multiple of dt_sys
dt = 1/50;               %Controller Step Size (sec)
dt_sys = dt/120;           %System Step Size (sec) 
dt_serial_TX1 = dt/120;     %Serial_TX Interface Step Size (sec) 
dt_serial_RX1 = dt/120;     %Serial_RX Interface Step Size (sec)
dt_serial_TX2 = dt/120;     %Serial_TX Interface Step Size (sec) 
dt_serial_RX2 = dt/120;     %Serial_RX Interface Step Size (sec)

bit_weight= (3.3*1000.00)/4095.00;