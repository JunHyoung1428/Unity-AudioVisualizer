## 📝 Introduction

- A project exploring various aspects using GetSpectrumData from AudioSource.


## 🎬 About Scenes

### 01.Deafault Visualizer
![Visualizer1-1](https://github.com/JunHyoung1428/Unity-AudioVisualizer/assets/154119773/afa90729-e2ad-4849-a744-2b1d3d2b1887)


### 02.FrequencyBand
![Visualizer2-2](https://github.com/JunHyoung1428/Unity-AudioVisualizer/assets/154119773/4453064d-dad8-482b-98b2-6f06f35ec570)

Group the SpectrumData into 8 audio frequency bands.
In this Project 43 herts per Sample

| Audio Frequency Subset | Frequency Range (Hz) | Sample Count | Calculated Frequency Range (Hz) |
|------------------------|----------------------|--------------|----------------------------------|
| SubBass                | 16 - 60              | 2            | 0 - 86                           |
| Bass                   | 60 - 250             | 4            | 87 - 258                         |
| LowMidrange            | 250 - 500            | 8            | 259 - 602                        |
| MidRange               | 500 - 2000           | 16           | 603 - 1290                       |
| HighMidrange           | 2000 - 4000          | 32           | 1291 - 2666                      |
| Presence               | 4000 - 6000          | 64           | 2667 - 5418                      |
| Brilliance             | 6000 - 20000         | 128          | 5419 - 10922                     |
| Highs                  | 20000 and above      | 256          | 10923 - 21930                    |


### 03.AudioResponseUI
![VisualizerUI](https://github.com/JunHyoung1428/Unity-AudioVisualizer/assets/154119773/d2a4fa67-37b1-4a6e-b05c-1de2ef7d7eb9)



## Feature plans
  - Add Buffer for Visualizer works lerp
  - Change Color or Light by SpectrumData


