<Cabbage>
form caption("Additive Waves"), size(300, 200)
</Cabbage>
<CsoundSynthesizer>
<CsOptions>
-n -d -m0d
</CsOptions>
<CsInstruments>
sr 	= 	44100 
ksmps 	= 	32
nchnls 	= 	2
0dbfs	=	1 

instr HANDLE
; parameters that change in Unity
    kparam chnget "sound" ; which wave type to play
    kfreq chnget "frequency"
    kamp chnget "amplitude"

    if(kparam = 0) igoto doublewave
        igoto sixwave

        sixwave:
        aOsc1   poscil  kamp, kfreq
        aOsc2   poscil  kamp/2, kfreq*2
        aOsc3   poscil  kamp/3, kfreq*3
        aOsc4   poscil  kamp/2, kfreq*2
        aOsc5   poscil  kamp/5, kfreq*5
        aOsc6   poscil  kamp/2, kfreq*6
        aOut = aOsc1 + aOsc2 + aOsc3 + aOsc4 + aOsc5 + aOsc6
        outs    aOut, aOut

        doublewave:
        aOsc1   poscil  kamp, kfreq
        aOsc2   poscil  kamp/2, kfreq*2
        aOut = aOsc1 + aOsc2
        outs    aOut, aOut

endin

;Initially tried three different instruments but could not change between them
instr SIXWAVE
    kfreq chnget "frequency"
    kamp chnget "amplitude"

    aOsc1   poscil  kamp, kfreq
    aOsc2   poscil  kamp/2, kfreq*2
    aOsc3   poscil  kamp/3, kfreq*3
    aOsc4   poscil  kamp/2, kfreq*2
    aOsc5   poscil  kamp/5, kfreq*5
    aOsc6   poscil  kamp/2, kfreq*6

    aOut = aOsc1 + aOsc2 + aOsc3 + aOsc4 + aOsc5 + aOsc6

    outs    aOut, aOut
endin

instr DOUBLEWAVE
    kfreq chnget "frequency"
    kamp chnget "amplitude"

    aOsc1   poscil  kamp, kfreq
    aOsc2   poscil  kamp/2, kfreq*2

    aOut = aOsc1 + aOsc2

    outs    aOut, aOut

endin

</CsInstruments>
<CsScore>
i"HANDLE" 0 [3600*12]
</CsScore>
</CsoundSynthesizer>