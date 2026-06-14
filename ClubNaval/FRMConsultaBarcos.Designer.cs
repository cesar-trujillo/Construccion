namespace ClubNaval
{
    partial class FRMConsultaBarcos
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
            this.dgvBarcos = new System.Windows.Forms.DataGridView();
            this.txtIdFiltro = new System.Windows.Forms.TextBox();
            this.cbxIdOwnerFiltro = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarcos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBarcos
            // 
            this.dgvBarcos.AllowUserToAddRows = false;
            this.dgvBarcos.AllowUserToDeleteRows = false;
            this.dgvBarcos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBarcos.Location = new System.Drawing.Point(30, 80);
            this.dgvBarcos.Name = "dgvBarcos";
            this.dgvBarcos.ReadOnly = true;
            this.dgvBarcos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBarcos.Size = new System.Drawing.Size(700, 250);
            this.dgvBarcos.TabIndex = 0;
            // 
            // txtIdFiltro
            // 
            this.txtIdFiltro.Location = new System.Drawing.Point(100, 30);
            this.txtIdFiltro.Name = "txtIdFiltro";
            this.txtIdFiltro.Size = new System.Drawing.Size(100, 20);
            this.txtIdFiltro.TabIndex = 1;
            // 
            // cbxIdOwnerFiltro
            // 
            this.cbxIdOwnerFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxIdOwnerFiltro.FormattingEnabled = true;
            this.cbxIdOwnerFiltro.Location = new System.Drawing.Point(320, 30);
            this.cbxIdOwnerFiltro.Name = "cbxIdOwnerFiltro";
            this.cbxIdOwnerFiltro.Size = new System.Drawing.Size(150, 21);
            this.cbxIdOwnerFiltro.TabIndex = 2;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(500, 28);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnActualizar.Location = new System.Drawing.Point(500, 360);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(90, 35);
            this.btnActualizar.TabIndex = 4;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.Location = new System.Drawing.Point(600, 360);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(90, 35);
            this.btnEliminar.TabIndex = 5;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "ID Barco";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(230, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "ID Propiet.";
            // 
            // FRMConsultaBarcos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 420);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.cbxIdOwnerFiltro);
            this.Controls.Add(this.txtIdFiltro);
            this.Controls.Add(this.dgvBarcos);
            this.Name = "FRMConsultaBarcos";
            this.Text = "Consulta de barcos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarcos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvBarcos;
        private System.Windows.Forms.TextBox txtIdFiltro;
        private System.Windows.Forms.ComboBox cbxIdOwnerFiltro;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
