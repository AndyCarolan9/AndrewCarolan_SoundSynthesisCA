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
    ifreq chnget "frequency"
    iamp chnget "amplitude"

    aOsc1   poscil  iamp, ifreq
    aOsc2   poscil  iamp/2, ifreq*2

    aOut = aOsc1 + aOsc2

    outs    aOut, aOut

endin

</CsInstruments>
<CsScore>
i"TEST" 0 [3600*12]
</CsScore>
</CsoundSynthesizer>