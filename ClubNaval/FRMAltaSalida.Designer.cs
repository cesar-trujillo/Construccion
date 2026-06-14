namespace ClubNaval
{
    partial class FRMAltaSalida
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFechaHora = new System.Windows.Forms.TextBox();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.cbxIdBarco = new System.Windows.Forms.ComboBox();
            this.cbxIdCapitan = new System.Windows.Forms.ComboBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(40, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha y Hora";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(40, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Destino";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(40, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "ID Barco";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(40, 264);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "ID Capitán";
            // 
            // txtFechaHora
            // 
            this.txtFechaHora.Location = new System.Drawing.Point(143, 51);
            this.txtFechaHora.Name = "txtFechaHora";
            this.txtFechaHora.ReadOnly = true;
            this.txtFechaHora.Size = new System.Drawing.Size(160, 20);
            this.txtFechaHora.TabIndex = 5;
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(143, 125);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(160, 20);
            this.txtDestino.TabIndex = 6;
            // 
            // cbxEstado
            // 
            // cbxIdBarco
            // 
            this.cbxIdBarco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxIdBarco.FormattingEnabled = true;
            this.cbxIdBarco.Location = new System.Drawing.Point(143, 196);
            this.cbxIdBarco.Name = "cbxIdBarco";
            this.cbxIdBarco.Size = new System.Drawing.Size(160, 21);
            this.cbxIdBarco.TabIndex = 7;
            // 
            // cbxIdCapitan
            // 
            this.cbxIdCapitan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxIdCapitan.FormattingEnabled = true;
            this.cbxIdCapitan.Location = new System.Drawing.Point(143, 264);
            this.cbxIdCapitan.Name = "cbxIdCapitan";
            this.cbxIdCapitan.Size = new System.Drawing.Size(160, 21);
            this.cbxIdCapitan.TabIndex = 8;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.Location = new System.Drawing.Point(667, 372);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(87, 40);
            this.btnGuardar.TabIndex = 9;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // FRMAltaSalida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.cbxIdCapitan);
            this.Controls.Add(this.cbxIdBarco);
            this.Controls.Add(this.txtDestino);
            this.Controls.Add(this.txtFechaHora);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FRMAltaSalida";
            this.Text = "Registrar salida";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFechaHora;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.ComboBox cbxIdBarco;
        private System.Windows.Forms.ComboBox cbxIdCapitan;
        private System.Windows.Forms.Button btnGuardar;
    }
}
