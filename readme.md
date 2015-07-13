#Flux Balance 
This project hosts the SBW Flux Balance Module. The program supports the [SBML Level 3 Flux Balance Proposal](http://sbml.org/Documents/Specifications/SBML_Level_3/Packages/Flux_Balance_Constraints_%28fbc%29), and so is able to read SBML files. 

![SBW Flux Balance](https://raw.github.com/fbergmann/FluxBalance/master/images/2012-11-11_-_FBA.png)

The program uses [LPsolve](http://lpsolve.sourceforge.net) as solver, and can so solve reasonably sized problems without any issue. 

With the latest [SBW version](http://128.208.17.26/fbergman/files/latest/SetupSBW.exe) installed, the program also allows to import Jarnac or [COPASI](http://copasi.org) files!

##Download
Github again supports binary releases, so you find releases right in the github releases menu. Alternatively, they will be available from [SourceForge](https://sourceforge.net/projects/sbw/upload/modules/FluxBalance/). The current version is:  

* version 1.10: <http://sourceforge.net/projects/sbw/files/modules/FluxBalance/SetupFBA-1.10.exe/download> (support for FBC V2)

* Version 1.9: <http://sourceforge.net/projects/sbw/files/modules/FluxBalance/SetupFBA-1.9.exe/download> (Corrected SBW export, added menu item, to allow to send L2 or L3 models to SBW).

* Version 1.8: <http://sourceforge.net/projects/sbw/files/modules/FluxBalance/SetupFBA-1.8.exe/download> (New is additional export of SBML Level 2 Models using either FBA or COBRA annotations).



##Previous Versions
[Version 1.7](https://github.com/fbergmann/FluxBalance/raw/master/releases/SetupFBA-1.7.exe) import of COBRA annotations, re-designed LP generation, so it will work for *large* models.
[Version 1.5](https://github.com/downloads/fbergmann/FluxBalance/SetupFBA-1.5.exe), fixed issues with L3 package annotations in L2 models.  
The [Version 1.4](https://github.com/downloads/fbergmann/FluxBalance/SetupFBA-1.4.exe), fixed issues with import from L2 annotation of FAME!  
[Version 1.3](https://github.com/downloads/fbergmann/FluxBalance/SetupFBA-1.3.exe), the first version from github is still available!  
The [first version](http://frank-fbergmann.blogspot.com/2009/03/fluxbalance-analysis-with-sbw.html) of the tool is still available from my [blog](http://frank-fbergmann.blogspot.com/), but essentially superceded by this project. 

![PoweredBySBW](https://raw.github.com/fbergmann/FluxBalance/master/images/SBW%20Logo-transparent.png) 
![PoweredBySBW](https://raw.github.com/fbergmann/FluxBalance/master/images/sbml-logo-70.jpg) 
 
## License
This project is open source and freely available under the [Simplified BSD](http://opensource.org/licenses/BSD-2-Clause) license. Should that license not meet your needs, please contact me. 

Copyright (c) 2013, Frank T. Bergmann  
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met: 

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.   
  
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.