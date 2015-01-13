﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;

namespace Framework.DataChangeManage
{
    public class CheckBoxSource : ChangeSource
    {
        public CheckBoxSource(params CheckBox[] controls)
        {
            Controls = new Dictionary<CheckBox, CheckState>();
            foreach (CheckBox control in controls)
            {
                Controls.Add(control, control.CheckState);
                control.CheckStateChanged += new EventHandler(Control_CheckStateChanged);
            }
        }

        private void Control_CheckStateChanged(object sender, EventArgs e)
        {
            bool changed = false;
            foreach (CheckBox control in Controls.Keys)
            {
                if (control.CheckState != Controls[control])
                {
                    changed = true;
                    break;
                }
            }
            if (changed)
                RaiseStatusChanged(ValueStatus.Dirty);
            else
                RaiseStatusChanged(ValueStatus.Clean);
        }

        protected Dictionary<CheckBox, CheckState> Controls { get; private set; }

        /// <summary>
        /// 重設狀態為「Clean」。
        /// </summary>
        public override void Reset()
        {
            foreach (CheckBox control in new Dictionary<CheckBox, CheckState>(Controls).Keys)
                Controls[control] = control.CheckState;
        }
    }
}
