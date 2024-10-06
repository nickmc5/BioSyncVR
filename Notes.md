LSL is a low-level technology that provides for the transfer of time-series information between programs and computers.

The LSL (Lab Streaming Layer) stack is devised of 
    > an LSL protocol for packet transfer of data
    > a library (liblsl) that is used to transmit and receive data via the protocol written in C++
    > LSL API used to interface with the library
    > Several wrappers for languages such as Python, MATLAB, Java, C#

Note that LSL stream are not found by TCP/IP but are rather discovered on the network based on the type of stream EEG
    > We can query based on content or name
    > We can have multiple readers of a stream (Recording, Reading, Processing)

Troubleshoot most errors with networking can be attributed to the firewall
    > Remember to flush the stream buffer when using LSL otherwise the program might crash unexpected

The stream source in LSL is called the outlet

The receiver resolves the advertised stream and is called the inlet


Brain Wave Notes:
    Skull serves as a low pass filter meaning the effective range of brain waves to be sampled is from 1-44 Hz our hardware should be able to accurately reproduce the single with the sample rate of ~256 Hz >> the Nyquist Rate of the target signals.

    Brain Wave Signals
    Name        Frequency (Hz)      Description
    Delta       1 - 4               Deep meditation, dreamless sleep
    Theta       4 - 8               During sleep and relaxation
    Alpha       7.5 - 13            Idle state, quiet and thoughful times
    Beta        13 - 30             active, waking state, alert and problem-solving
    Gamma       30 - 44             deep concentration and consciousness

Photoplethysmography (PPG):
    A PPG sensor makes use of a low-intensity IR light that is absorbed by tissues and blood. Note that the IR light is more likely absorbed by blood which in turn is can be used to measure blood flow. The voltage signal detected by the PPG sensor is proportional to the quantity of blood flowing through the material.

    Note that the variations in blood volume are proportional to the heart beat. The DC component of the signal show minor changes in respiration.

Muse 2 Headset

    measures EEG data, acceleration, gyroscopic data, and photoplethysmography (PPG) data

        Data input from EEG via LSL
        TYPE            Sample Rate       # Channels          Names                            Range:
        EEG:              ~256 Hz        Five Channels     [TP9, AF8, AF9, TP10, Right AUX]         (-1000, 1000) in mV?
        Acceleromter:     ~50 Hz         Three DOF         [X, Y, Z]                                (-1, 1) 
        Gyroscope:        ~50 Hz         Three DOF         [X, Y, Z]                                (-1, 1)
        PPG:              ~64 Hz         Three Channels    [PPG1, PPG2, PPG3]
