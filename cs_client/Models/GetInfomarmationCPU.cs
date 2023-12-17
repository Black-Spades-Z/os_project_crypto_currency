using System;
using System.Diagnostics;
using Gtk;

class GetCPUInformation
{
    public static void GetCPUInfo(TextView textView)
    {

        TextBuffer textBuffer = textView.Buffer;

        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "/bin/bash";
        startInfo.Arguments = "-c 'lscpu'"; // Use 'lscpu' command to get CPU info
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        process.StartInfo = startInfo;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        process.Kill();
        string[] lines = output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        // Take the first 18 lines
        string subset = string.Join(Environment.NewLine, lines.Take(18));
        textBuffer.Text += subset;


    }
}
