﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace Cipher//Названия переменных, объектов и методов сделаны такими специально, обычно я их нормально называю :-) 
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            for (int j = 0; j < s.Length - 2; j += 2)
            {
                char ch = s[j];
                s = s.Remove(j, 1);
                s = s.Insert(j + 1, ch.ToString());
            }
            int i = 0;
            foreach (char c in s)
            {
                int a = (int)c;
                if (i % 2 == 0)
                {
                    a += (i + 10);
                }
                else
                {
                    a -= (i + 10);
                }
                while (a < 0) a += 65536;
                while (a > 65535) a -= 65536;
                s = s.Remove(i, 1);
                s = s.Insert(i, ((char)a).ToString());
                i++;
            }
            s = new string(s.ToCharArray().Reverse().ToArray());
            string s1 = "";
            foreach (char c in s)
            {
                string s2 = ((int)c).ToString();
                while (s2.Length < 5) s2 = '0' + s2;
                s1 += s2;
            }
            s = s1;
            int[,] k = new int[100, 3];
            Random x = new Random();
            for (int j = 0; j < 100; j++)
                for (int f = 0; f < 3; f++)
                    k[j, f] = x.Next(0, 10);
            k[0, 0] = x.Next(101, 900);
            k[0, 1] = x.Next(11, 100);

            int z = x.Next(91, 100);
            for (int j = 0; j < 10; j++)
            {
                int d = x.Next(1, 4);
                k[z, 0] = d;
                if (d == 1)
                {
                    int p = x.Next(1, 10);
                    k[z, 1] = p;
                    p *= k[0, 1];
                    while (s.Length < p)
                        p -= s.Length;
                    if (p == s.Length) p--;

                    StringBuilder sb = new StringBuilder(s);
                    for (int f = 0; f < p; f++)
                        sb.Remove(0, 1).Append(s[f]);
                    s = sb.ToString();
                }
                if (d == 2)
                {
                    int p = x.Next(1, 10);
                    k[z, 1] = p;
                    p *= k[0, 1];
                    while (s.Length < p)
                        p -= s.Length;
                    if (p == s.Length) p--;

                    p = s.Length - p;
                    StringBuilder sb = new StringBuilder(s);
                    for (int f = 0; f < p; f++)
                        sb.Remove(0, 1).Append(s[f]);
                    s = sb.ToString();
                }
                if (d == 3)
                {
                    int p = x.Next(2, 10);
                    k[z, 1] = p;
                    int f = 0;
                    string sk = "";
                    while (s[f] == '0')
                    {
                        sk += 0;
                        f++;
                    }
                    BigInteger b = BigInteger.Parse(s) * p;
                    s = sk + b.ToString();
                }
                int y = x.Next(1, 10);
                k[z - y, 2] = y;
                if (j != 9) z -= y;
            }
            k[0, 2] = z;

            textBox2.Text = s;

            z = k[0, 2]; ;
            for (int j = 0; j < 10; j++)
            {
                int d = k[z, 0];
                int p = k[z, 1];
                if (d == 2)
                {

                    p *= k[0, 1];
                    while (s.Length < p)
                        p -= s.Length;
                    if (p == s.Length) p--;
                    StringBuilder sb = new StringBuilder(s);
                    for (int f = 0; f < p; f++)
                        sb.Remove(0, 1).Append(s[f]);
                    s = sb.ToString();
                }
                if (d == 1)
                {

                    p *= k[0, 1];
                    while (s.Length < p)
                        p -= s.Length;
                    if (p == s.Length) p--;
                    p = s.Length - p;
                    StringBuilder sb = new StringBuilder(s);
                    for (int f = 0; f < p; f++)
                        sb.Remove(0, 1).Append(s[f]);
                    s = sb.ToString();
                }
                if (d == 3)
                {
                    int f = 0;
                    string sk = "";
                    while (s[f] == '0')
                    {
                        sk += 0;
                        f++;
                    }
                    BigInteger b = BigInteger.Parse(s) / p;
                    s = sk + b.ToString();

                }
                z += k[z, 2];
            }
            s1 = "";
            for (int j = 0; j < s.Length - 4; j += 5)
            {
                int a = Convert.ToInt32(s[j].ToString() + s[j + 1].ToString() + s[j + 2].ToString() + s[j + 3].ToString() + s[j + 4].ToString());
                s1 += ((char)a).ToString();
            }
            s = s1;
            s = new string(s.ToCharArray().Reverse().ToArray());
            i = 0;
            foreach (char c in s)
            {
                int a = (int)c;
                if (i % 2 == 0)
                {
                    a -= (i + 10);
                }
                else
                {
                    a += (i + 10);
                }
                while (a < 0) a += 65536;
                while (a > 65535) a -= 65536;
                s = s.Remove(i, 1);
                s = s.Insert(i, ((char)a).ToString());
                i++;
            }
            for (int j = 0; j < s.Length - 2; j += 2)
            {
                char ch = s[j];
                s = s.Remove(j, 1);
                s = s.Insert(j + 1, ch.ToString());
            }
            textBox3.Text = s;
        }
    }
}
