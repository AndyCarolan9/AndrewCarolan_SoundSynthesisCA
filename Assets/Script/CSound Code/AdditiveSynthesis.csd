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

instr 2SineWaves
    kfreq chnget "frequency"
    kamp chnget "amplitude"

    ibasefrq = cpspch(kfreq)
    ibaseamp = ampdbfs(kamp)

    aOsc1   poscil  ibaseamp, ibasefrq
    aOsc2   poscil  ibaseamp/2, ibasefrq*2

    aOut = aOsc1 + aOsc2

    outs    aOut, aOut

endin

</CsInstruments>
<CsScore>
i"2SineWaves" 0 [3600*12]
</CsScore>
</CsoundSynthesizer>