//global variables for settings
var showList                    : boolean   = false;
public var listEntrySelected    : int;
static var listEntry            : int       = 2;
static var defaultEntryNumber   : int       = 0;
var generalListStyle            : GUIStyle  = new GUIStyle();
 
//dropdown menu content
var listColours : GUIContent[]; listColours = new GUIContent[25];
listColours[0]  = new GUIContent("N/A");
listColours[1]  = new GUIContent("[blank]");
listColours[2]  = new GUIContent("App ready...");
listColours[3]  = new GUIContent("Ding Dong");
listColours[4]  = new GUIContent("Which");
listColours[5]  = new GUIContent("Golden Snitch");
listColours[6]  = new GUIContent("Itch");
listColours[7]  = new GUIContent("Wiki");
listColours[8]  = new GUIContent("Succeed in business.");
listColours[9]  = new GUIContent("Another Thing");
listColours[10]  = new GUIContent("Another Thing");
listColours[11]  = new GUIContent("Another Thing"); 
 
 
private var prevSelected:int;
 
 
generalListStyle.padding.left = generalListStyle.padding.right = generalListStyle.padding.top = generalListStyle.padding.bottom = 4;
 
var dropdownListHash : int = "DropdownList".GetHashCode();

private var controlID;
 
//      List(Rect(0,0,100,100),         false,              0,              GUIContent("Select Colour"),    listColours             "button",               "box",          generalListStyle)
function List(position : Rect, expandList : boolean, listEntry : int, defaultListEntry : GUIContent, listToUse : GUIContent[], buttonStyle : GUIStyle, boxStyle : GUIStyle, listStyle : GUIStyle)
{
   
    controlID = GUIUtility.GetControlID(dropdownListHash, FocusType.Passive);
    var done : boolean = false;
   
    if(Event.current.GetTypeForControl(controlID) == EventType.mouseDown)
    {
    if (position.Contains(Event.current.mousePosition))
        {
        GUIUtility.hotControl = controlID;
        showList = !showList;
        }
    }
   
    if(Event.current.GetTypeForControl(controlID) == EventType.mouseDown && !position.Contains(Event.current.mousePosition))
    {
        GUIUtility.hotControl = controlID;
    }              
   
    GUI.Label(position, defaultListEntry, buttonStyle);
   
    if(expandList)
    {
    //list rectangle
    var listRect : Rect = new Rect(position.x, position.y+20, position.width, listStyle.CalcHeight(listToUse[0], 1.0f) * listToUse.Length);
    GUI.Box(listRect, "", boxStyle);
   
    listEntrySelected = GUI.SelectionGrid(listRect, listEntrySelected, listToUse, 1, listStyle);
    listEntry = listEntrySelected;
   
        if(listEntrySelected != defaultEntryNumber && !position.Contains(Event.current.mousePosition))
        {
        GUIUtility.hotControl = controlID;
        showList = !showList;
        defaultEntryNumber = listEntrySelected;
        }    
    }
}
 
function OnGUI()
{
    GUI.Label(Rect(600,10, 100, 20), listColours[listEntrySelected].text);
    List(Rect(200, 10, 200, 20), showList, listEntry, GUIContent(listColours[listEntrySelected].text), listColours, "button", "box", generalListStyle);
}

function Update(){
	if(listEntrySelected != prevSelected ){
		GameObject.Find("Network").SendMessage("selectText", listEntrySelected);
		prevSelected = listEntrySelected;
		Debug.Log("text selected in dropdown");
	}
	
}



