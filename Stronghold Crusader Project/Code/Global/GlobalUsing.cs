//Global using so that I am not constantly calling using at the start of every file

//System libraries
global using System;
global using System.IO;
global using System.Text;
global using System.Collections.Generic;
global using System.Linq;

//Monogame/XNA frameworks
global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Input;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Content;

//JSON Framework
global using Newtonsoft.Json;

//Subfolders of each code
global using Stronghold_Crusader_Project.Code.Buildings;
global using Stronghold_Crusader_Project.Code.Mapping;
global using Stronghold_Crusader_Project.Code.Materials;
global using Stronghold_Crusader_Project.Code.Global;
global using Stronghold_Crusader_Project.Code.Player;
global using Stronghold_Crusader_Project.Code.User_Input;
global using Stronghold_Crusader_Project.Code.Global.Other;
global using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu;
global using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;
global using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;
global using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Other;

//Methods that need to be accessed globally
global using static Stronghold_Crusader_Project.Code.Global.GlobalConfig;
global using static Stronghold_Crusader_Project.Code.Global.Other.EventLogger;
global using static Stronghold_Crusader_Project.Code.Global.Other.Camera2D;
global using static Stronghold_Crusader_Project.Code.User_Input.InputManager;
global using static Stronghold_Crusader_Project.Code.Mapping.MapHandler;

//Any buttons that are global
global using static Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Other.GlobalNavigationButton;
global using static Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Other.GlobalBasicTextButton;


