"""Example program to show how to read a multi-channel time series from LSL."""

from pylsl import StreamInlet, resolve_stream
from scipy import signal, stats, fftpack
import pylab as plt
import numpy as np

'''
Data input from EEG via LSL
TYPE            Sample Rate       # Channels          Names
EEG:              ~256 Hz        Five Channels     [TP9, AF8, AF9, TP10, Right AUX]
Acceleromter:     ~50 Hz         Three DOF         [X, Y, Z]
Gyroscope:        ~50 Hz         Three DOF         [X, Y, Z]
PPG:              ~64 Hz         Three Channels    [PPG1, PPG2, PPG3]
'''
fs = 256
N = num_samples = fs * 2
T = 1 / 256.0

CH_TP9 = 0
CH_AF8 = 1
CH_AF9 = 2
CH_TP10 = 3
CH_AUX = 4

X = 0
Y = 1
Z = 2

CH_PPG1 = 0
CH_PPG2 = 1
CH_PPG3 = 2

def main():
    # first resolve an EEG stream on the lab network
    print("looking for an EEG stream...")
    eeg_streams = resolve_stream('type', 'EEG')
    # accel_streams = resolve_stream('type', 'Accelerometer')
    # gyro_streams = resolve_stream('type', 'Gyroscope')
    # ppg_streams = resolve_stream('type', 'PPG')

    # create a new inlet to read from the stream
    eeg = StreamInlet(eeg_streams[0])

    # Filter Design
    b, a = signal.butter(10, 50, 'lp', fs=256)

    # Plot interactive
    plt.ion()
    # Plots on
    fig, (ax1, ax2, ax3, ax4) = plt.subplots(4, 1)
    t = sig = np.arange(0, num_samples)
    ax1.plot(t, sig)
    ax2.plot(t, sig)
    ax3.plot(t, sig)
    ax4.plot(t, sig)
    ax1.set_title('Signal')
    ax2.set_title('Filtered')
    ax3.set_title('Z-Score')

    while True:
        # get a new sample (you can also omit the timestamp part if you're not interested in it)
        sample, timestamp = eeg.pull_chunk(timeout=100.0, max_samples=num_samples)
        x = np.array(sample)
        sig = x[:, CH_AF8]
        print('EEG AF8')
        print(sig)
        print(t)
        ax1.clear()
        ax1.plot(t, sig)

        print('Filtered')
        sig_fil = signal.filtfilt(b, a, sig)
        ax2.clear()
        ax2.plot(t, sig_fil)
        print(sig_fil)

        print('EEG Z-Score')
        z = stats.zscore(sig_fil)
        ax3.clear()
        ax3.plot(t, z)
        print(z)

        yf = fftpack.fft(z)
        xf = np.linspace(0.0, 1.0/(2.0*T), 128)
        ax4.clear()
        ax4.plot(xf, 2.0/N * np.abs(yf[:128]))
        plt.pause(0.1)
        



if __name__ == '__main__':
    main()