/// <summary>
/// Global using is to prevent having to have "using" at the top of every file
/// </summary>

#region System Libraries
// Libaries that are used through system

global using System;
global using System.IO;
global using System.Text;
global using System.Collections.Generic;
global using System.Linq;

#endregion

#region XNA Framework
//Framework from XNA that Monogame uses

global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Input;
global using Microsoft.Xna.Framework.Graphics;
global using Microsoft.Xna.Framework.Content;

#endregion

#region Other Frameworks
//Frameworks that are outside of Xna

global using Newtonsoft.Json;

#endregion

#region Namespaces
//Namespaces are the folders that the code is in

global using Stronghold_Crusader_Project.Code.Global;
global using Stronghold_Crusader_Project.Code.Game;
global using Stronghold_Crusader_Project.Code.Mapping;
global using Stronghold_Crusader_Project.Code.Units;
global using Stronghold_Crusader_Project.Code.User_Input;
global using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu;
global using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Buttons;
global using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.DrawerTypes;
global using Stronghold_Crusader_Project.Code.User_Input.Navigation_Menu.Menus;

#endregion

#region Stactic Helpers
//Any class that is stactic and can be accessed throughout all classes
global using static Stronghold_Crusader_Project.Code.Global.GlobalConfig;
global using static Stronghold_Crusader_Project.Code.Global.EventLogger;

#endregion