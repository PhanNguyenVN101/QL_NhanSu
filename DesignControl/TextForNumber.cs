﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesignControl
{
    public class TextForNumber:TextBox
    {
        public TextForNumber()
        {
            this.KeyPress += TextForNumber_KeyPress;
            this.BackColor = Color.FromArgb(46, 51, 73);
            this.ForeColor = Color.White;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
        }

        private void TextForNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
