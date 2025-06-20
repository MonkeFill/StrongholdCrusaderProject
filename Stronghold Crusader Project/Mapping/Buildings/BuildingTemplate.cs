using System.Collections.Generic;
using Stronghold_Crusader_Project.Materials;

namespace Stronghold_Crusader_Project.Mapping.Buildings;

public abstract class BuildingTemplate
{
    string Name { get; set; }
    int GoldCost  { get; set; }
    List<MaterialTemplate>  MaterialRequired { get; set; }
    int Health  { get; set; }
    int MaxHealth { get; set; }
    
    
    
    public void DisplayBuilding()
    {
        
    }
    public void CreateBuilding()
    {
        
    }
    public void DestroyBuilding()
    {
        
    }
    public void ViewInBuilding()
    {
        
    }
    
}

