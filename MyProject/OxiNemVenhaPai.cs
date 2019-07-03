using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Robocode;
using System.Drawing;

namespace TRB7_OxiNemVenhaPai
{
    // The name of your robot is MyRobot, and the robot type is Robot
    class OxiNemVenhaPai : AdvancedRobot
    {
        //int dist = 50;
        // The main method of your robot containing robot logics
        public override void Run()
        {
            // Set
            BulletColor = (Color.Red);
            //Cor do Corpo
            BodyColor = (Color.White);
            //Cor da Arma
            GunColor = (Color.Orange);
            //Cor do Radar
            RadarColor = (Color.White);
            //Cor do Scan
            ScanColor = (Color.Blue);





            // -- Initialization of the robot --

            // Here we turn the robot to point upwards, and move the gun 90 degrees
            //TurnLeft(Heading - 90);
            //TurnGunRight(90);

            // Infinite loop making sure this robot runs till the end of the battle round
            while (true)
            {
                //TurnGunRight(5);
                // -- Commands that are repeated forever --

                // Move our robot 5000 pixels ahead
                SetAhead(100);
                SetTurnGunRight(90);
                SetTurnLeft(180);
                Execute();

                //TurnRadarRight(360);
                //Ahead(100);
                //TurnGunRight(360);
                //Back(100);
                // Turn the robot 90 degrees
                // TurnRight(40);

                // Our robot will move along the borders of the battle field
                // by repeating the above two statements.
            }
        }

        int targetX = int.MinValue;
        int targetY = int.MinValue;

        // Detecta os outros robôs 
        private double radtodeg(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        private double degtorad(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            // double max = 100;
            // We fire the gun with bullet power = 1
            // Fire(Rules.MAX_BULLET_POWER);
            //Fire(1);



            //if (e.Energy < max)
            //{
            //    max = e.Energy;
            //    ajusteCanhao(e.Bearing, max, Energy);
            //}
            //else if (e.Energy >= max)
            //{
            //    max = e.Energy;
            //    ajusteCanhao(e.Bearing, max, Energy);
            //}
            //else if (Others == 1)
            //{
            //    max = e.Energy;
            //    ajusteCanhao(e.Bearing, max, Energy);
            //}

            //ajusteCanhao();
            if (e.Distance < 150)
            {
                Fire(Rules.MAX_BULLET_POWER);
            }
            else if (e.Distance >= 150 && e.Distance <= 300)
            {
                Fire(2);
            }
            else
            {
                Fire(1);
            }
            //{
            //}
            double angle = degtorad(Heading);
            // Calculate the coordinates of the robot
            targetX = (int)(X + Math.Sin(angle) * e.Distance);
            targetY = (int)(Y + Math.Cos(angle) * e.Distance);
        }

        public override void OnPaint(IGraphics g)
        {
            // Draw a line from our robot to the scanned robot
            g.DrawLine(new Pen(Color.Red), targetX, targetY, (int)X, (int)Y);

            // Draw a filled square on top of the scanned robot that covers it
            g.DrawRectangle(new Pen(Color.Red), targetX - 20, targetY - 20, 40, 40);
        }

        public void ajusteCanhao(double PosIni, double energiaIni, double minhaEnergia)
        {
            double Distancia = PosIni;
            double Coordenadas = Heading + PosIni - GunHeading;
            double PontoQuarenta = (energiaIni / 4) + .1;
            if (!(Coordenadas > -180 && Coordenadas <= 180))
            {
                while (Coordenadas <= -180)
                {
                    Coordenadas += 360;
                }
                while (Coordenadas > 180)
                {
                    Coordenadas -= 360;
                }
            }
            TurnGunRight(Coordenadas);

            if (Distancia > 200 || minhaEnergia < 15 || energiaIni > minhaEnergia)
            {
                Fire(1);
            }
            else if (Distancia > 50)
            {
                Fire(2);
            }
            else
            {
                Fire(PontoQuarenta);
            }
        }

    }

    // Bearing = Retorna o ângulo do robô adversário em relação ao seu robô
    // Distance = Retorna a distancia do robo adversario em relação ao seu robô
    // Energy = Retorna a enrgia
    // Retorna o ângulo em graus do adversário em relação a tela

}

