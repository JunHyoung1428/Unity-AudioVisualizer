## 📝 Introduction

- A project exploring various aspects using **GetSpectrumData** from **AudioSource**.

<br/> <br/> 

      
## 🎬 About Scenes

  - ### 01.Deafault Visualizer
![Visualizer1-2](https://github.com/JunHyoung1428/Unity-AudioVisualizer/assets/154119773/dd94ca4a-78fa-4cdd-84ce-9e8e109fe517)

***
 - ### 02.FrequencyBand
![Visualizer2-2](https://github.com/JunHyoung1428/Unity-AudioVisualizer/assets/154119773/4453064d-dad8-482b-98b2-6f06f35ec570)
![Visualizer2-4](https://github.com/JunHyoung1428/Unity-AudioVisualizer/assets/154119773/e5427532-473e-4deb-9748-43cbecbbfddc)

Group the SpectrumData into **8 audio frequency bands**.  
In this Project 43 hertz per Sample

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

***
- ### 03.AudioResponseUI
![VisualizerUI](https://github.com/JunHyoung1428/Unity-AudioVisualizer/assets/154119773/d2a4fa67-37b1-4a6e-b05c-1de2ef7d7eb9)
😎

***

- ### 04.MicrophoneListener

![Visualizer4-1](https://github.com/JunHyoung1428/Unity-AudioVisualizer/assets/154119773/c728136c-8ba5-4a3a-81db-371b0a0c0420)

Response by Mic Volume

<br/> <br/> 
## 💡Feature plans
  - Add Buffer for Visualizer works lerp
  - Change Color or Light by SpectrumData


