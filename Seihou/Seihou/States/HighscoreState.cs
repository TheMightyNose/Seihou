﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Data.SqlClient;

namespace Seihou
{
    public class HighscoreState : State
    {
        SpriteBatch sb;
        ListBox scoreBox;
        int printHeight = 0;
        const int maxElements = 17;

        List<Control> controls = new List<Control>();
        Textbox textbox;
        
        string[] text;

        public HighscoreState(StateManager sm, ContentManager cm, SpriteBatch sb, GraphicsDeviceManager gdm) : base(sm, cm, sb, gdm)
        {
            string conStr = $"Connection Timeout=30;Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={System.IO.Directory.GetCurrentDirectory()}\\GameData.mdf;Integrated Security=True;";

            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    con.Open();

                    Console.WriteLine("Connection open...");

                    List<string> print = new List<string>();
                    var c = new SqlCommand("SELECT * FROM [Highscores] ORDER BY [score] DESC");
                    c.Connection = con;
                    c.CommandTimeout = 1;

                    

                    using (SqlDataReader reader = c.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string score = reader[0].ToString();
                            string modo = reader[2].ToString();
                            string name = reader[1].ToString();

                            while (score.Length < 14) score = score.Insert(0, "0");
                            print.Add($"{score} | {modo} : {name}");
                        }
                    }
                    text = print.ToArray();
                }
            }
            catch(Exception e)
            {
                text = new string[] {"No highscores available",e.Message}; 
            }

            this.sb = sb;
            scoreBox = new ListBox(new Vector2(100, 100), new Vector2(1000, 500),this.sb);
            scoreBox.background = new Color(40, 40, 40);

            var s = new Vector2(100, 30);
            var p = new Vector2(930, 650);
            controls.Add(new Button(p, s, sb, MoveUp, "UP"));
            controls.Add(new Button(new Vector2(p.X + s.X,p.Y), s, sb, MoveDown, "DOWN"));
            controls.Add(new Textbox(new Vector2(100, 650), sb));

            UpdateText();
        }

        private void MoveUp(object sender)
        {
            if (printHeight > 0)
            {
                printHeight--;
                UpdateText();
            }
        }

        private void MoveDown(object sender)
        {
            if (text.Length - printHeight > maxElements)
            {
                printHeight++;
                UpdateText();
            }
        }

        private void UpdateText()
        {
            List<string> part = new List<string>();
            for (int i = printHeight; i < text.Length; i++)
            {
                part.Add(text[i]);
                if (part.Count >= maxElements) break;
            }
            scoreBox.text = part.ToArray();
        }


        public override void Draw(GameTime gt)
        {
            sb.DrawString(ResourceManager.fonts["DefaultFont"], "Score             Mode : name", new Vector2(scoreBox.pos.X, 50), Color.White);
            scoreBox.Draw(gt);
            foreach (var c in controls) c.Draw(gt);     
        }
        
        public override void Update(GameTime gt)
        {
            foreach (var c in controls) c.Update(gt);
            UpdateText();
        }
        
        public override void OnStart()
        {
        }
        
        public override void OnExit()
        {
        }
    }  
}
