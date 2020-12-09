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

instr TEST
    kfreq chnget "frequency"
    kamp chnget "amplitude"

    aOsc1   poscil  kamp, kfreq
    aOsc2   poscil  kamp/2, kfreq*2

    aOut = aOsc1 + aOsc2

    outs    aOut, aOut

endin

</CsInstruments>
<CsScore>
i"TEST" 0 [3600*12]
</CsScore>
</CsoundSynthesizer>