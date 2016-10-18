using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Record
{
    class ProcessCl : IProcess
    {
        public void openFileToShow(string filingPath)
        {
            string extention=Path.GetExtension(filingPath);
            if (extention.Equals(".txt"))
            {
                startNotePad(filingPath);
            }
            else if (extention.Equals(".doc") || extention.Equals(".docx"))
            {
                startMSWord(filingPath);
            }
            else if (extention.Equals(".xls"))
            {
                startMSExcel(filingPath);
            }
            else if (extention.Equals(".pptx") || extention.Equals(".ppt"))
            {
                startMSPowerpoint(filingPath);
            }
            else if (extention.Equals(".pdf"))
            {
                startPdf(filingPath);
            }
            else if (extention.Equals(".jpg"))
            {
                startPhoto(filingPath);
            }

            else if (extention.Equals(".mp3") || extention.Equals(".mp4"))
            {
                startAudioVedio(filingPath);
            }           
        }
        public void startNotePad(string filingPath)
        {
             Process notePad = new Process();
             notePad.StartInfo.FileName = "notepad.exe";
             notePad.StartInfo.Arguments = @filingPath;
             notePad.Start();
        }

        public void startMSWord(string filingPath)
        {
             Process msword = new Process();
             msword.StartInfo.FileName = "WINWORD.exe";
             msword.StartInfo.Arguments = @filingPath;
             msword.Start();
         }

        public void startMSExcel(string filingPath)
           {
            Process msexcel = new Process();
            msexcel.StartInfo.FileName = "EXCEL.exe";
            msexcel.StartInfo.Arguments = @filingPath;
            msexcel.Start();

           }

        public void startMSPowerpoint(string filingPath)
          {
            Process mspowerpoint = new Process();
            mspowerpoint.StartInfo.FileName = "POWERPNT.exe";
            mspowerpoint.StartInfo.Arguments = @filingPath;
            mspowerpoint.Start();
          }

        public void startPdf(string filingPath)
        {
            Process pdf = new Process();
            pdf.StartInfo.FileName = "AcroRd32.exe";
            pdf.StartInfo.Arguments = @filingPath;
            pdf.Start();
        }

        public void startAudioVedio(string filingPath)
         {
            Process player = new Process();
            player.StartInfo.FileName = "wmplayer.exe";
            player.StartInfo.Arguments = @filingPath;
            player.Start();
         }

        public void startPhoto(string filingPath)
        {
            Photo_Viewer ob = new Photo_Viewer();
            ob.showPhoto(@filingPath);
            ob.ShowDialog();
        }
    }
}
