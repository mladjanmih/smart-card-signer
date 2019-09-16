using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SmartCardSignerApi;
using System.Threading;
using System.Threading.Tasks;

namespace SmartCardReader.Windows
{
    partial class SmartCardReader
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private CancellationTokenSource source = new CancellationTokenSource();
        private Task hostTask = null;
        private IHost host = null;
        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                source.Cancel();
         //       hostTask.Dispose();
         //       source.Cancel();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.Location = new System.Drawing.Point(57, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(368, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Smart Card Signer App";

            this.components = new System.ComponentModel.Container();
            this.Controls.Add(label1);
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 224);
            this.Text = "Smart Card Reader";
            this.ResumeLayout(false);
            this.PerformLayout();


            var builder = Host.CreateDefaultBuilder()
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
            host = builder.Build();
            hostTask = Task.Run(() => host.RunAsync(source.Token));
        }

        #endregion
        private System.Windows.Forms.Label label1;
    }
}

