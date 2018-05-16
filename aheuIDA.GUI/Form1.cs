using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using aheuIDA;

namespace aheuIDA.GUI
{
    public partial class Form1 : Form
    {
        AheuiDebugger _aheui;
        bool _started;
        Queue<string> inputs;
        int i;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            if (!_started)
            {
                _aheui = new AheuiDebugger(txtCode.Text);
                inputs = new Queue<string>(txtInput.Text?.Replace("\r\n", "\n").Split('\n'));
                _aheui.NeedInput += _aheui_NeedInput;
                _aheui.NeedOutput += _aheui_NeedOutput;
                _started = true;
            }
            if (_aheui.IsExited) return;
            _aheui.Step();
            DisplayStorage();
        }

        private void _aheui_NeedOutput(int value, bool isNumeric)
        {
            if (isNumeric) txtOut.AppendText(value.ToString());
            else txtOut.AppendText(((char)value).ToString());
        }

        private int _aheui_NeedInput(bool isNumeric)
        {
            if (isNumeric)
                return Convert.ToInt32(inputs.Dequeue());
            else
                return Convert.ToChar(inputs.Dequeue());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOut.Clear();
            txtStorage.Clear();
            _started = false;
        }

        private void DisplayStorage()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"위치: ({_aheui.X}, {_aheui.Y})");
            sb.AppendLine($"명령: {_aheui.CurrentCode.Letter}");
            foreach (var (c, s) in _aheui.StacksWithKey)
            {
                if (c == _aheui.CurrentStorage)
                    sb.Append('>');
                sb.AppendLine($"{Hangul.CombineLetter('ㅇ', 'ㅏ', c)}: {string.Join(", ", s)}");
            }
            this.txtStorage.Text = sb.ToString();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            while (!_aheui.IsExited)
            {
                _aheui.Step();
                DisplayStorage();
            }
        }
    }
}
