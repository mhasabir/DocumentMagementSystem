using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Record
{
    interface IProcess
    {
           void openFileToShow(string filingPath);
           void startNotePad(string filingPath);
           void startMSWord(string filingPath);
           void startMSExcel(string filingPath);
           void startMSPowerpoint(string filingPath);
           void startPdf(string filingPath);
           void startAudioVedio(string filingPath);
           void startPhoto(string filingPath);
    }
}
