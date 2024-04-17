using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseSystems;
using BEPUphysics.Character;
using BEPUphysics.CollisionTests;
using BEPUphysics.Constraints;
using BEPUutilities;
using BEPUutilities.DataStructures;
using Deterministic.FixedPoint;
using Godot;


public partial class PhysicsDemoSpace : Level
{
	Global global;  VBoxContainer UIContainer;
  RichTextLabel WorldPos; RichTextLabel Contacts; RichTextLabel HasSupport; 
  RichTextLabel Overlaps; RichTextLabel Thresholds;

  public override void _Ready()
	{
		base._Ready();
    global = GetTree().Root.GetNode<Global>("Global");
    UIContainer = GetTree().Root.GetNode<VBoxContainer>("PhysicsDemoSpace/DebugUI/VBoxContainer");

    Thresholds = UIContainer.GetNode<RichTextLabel>("Thresholds");
    WorldPos = UIContainer.GetNode<RichTextLabel>("WorldPos");
    Contacts = UIContainer.GetNode<RichTextLabel>("Contacts"); 
    HasSupport = UIContainer.GetNode<RichTextLabel>("HasSupport");
    Overlaps = UIContainer.GetNode<RichTextLabel>("Overlaps");
  }

  public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
    var playerPos = global.Player.Body.Position;
    if (playerPos.Y < (fp)(-72)) {
      global.Player.Body.Position = (new BEPUutilities.Vector3(0,0,16)) + global.Player.colShape.PosOffset;
		}

    ////global.Player.Body.Position = (new BEPUutilities.Vector3(0, -1, 16)) + global.Player.colShape.PosOffset;
    ////global.Player.Body.Gravity = new fp3((fp)0, -F64.C1, (fp)0) * 50;
    ////global.Player.Body.Mass = 1; 

    ////global.Player.Controller.ContactCategorizer.  


    WorldPos.Text = "\nWorldPos: " + playerPos.ToString();


    fp[] contactTresh = CharacterContactCategorizer.GetThresholds();
    Thresholds.Text = "\nTractionThreshold: " + contactTresh[0] +
              "\n SupportThreshold: " + contactTresh[1] +
              "\n    HeadThreshold: " + contactTresh[2];


    //CharacterContact[][] contacts = SupportFinder.GetContacts();
    //string contactsText = "\nContacts PenDepth: ";
    //for (int i = 0; i < contacts.Length; i++)
    //{ 
    //  for (int j = 0; j < contacts[i].Length; j++)
    //  {
    ////contactsText += "\n" + contacts[i][j].Contact.GetType().Name;
    ////contactsText += "\n" + contacts[i][j].Collidable.GetOwner().ToString();
    //  }
    //}
    //Contacts.Text = contactsText;


    var supports = global.Player.Controller.SupportFinder;
    Contacts.Text = "\n[color=#FF66FF]Contact Lists: [/color]";
    string contactsText = "\n[color=#FF66FF]Contact Lists: [/color]";

    for (int i = 0; i < supports.Supports.Count; i++)
    { contactsText += "\n" + supports.Supports[i].GetType().Name; }
    if (supports.Supports.Count == 0) Contacts.Text += "\nSupports: N/A";

    for (int i = 0; i < supports.TractionSupports.Count; i++)
    { contactsText += "\n" + supports.TractionSupports[i].Contact.PenetrationDepth.AsFloat; }
    if (supports.TractionSupports.Count == 0) Contacts.Text += "\nTraction: N/A";

    for (int i = 0; i < supports.SideContacts.Count; i++)
    { contactsText += "\n" + supports.SideContacts[i].GetType().Name; }
    if (supports.SideContacts.Count == 0) Contacts.Text += "\nSide: N/A";

    for (int i = 0; i < supports.HeadContacts.Count; i++)
    { contactsText += "\n" + supports.HeadContacts[i].GetType().Name; }
    if (supports.HeadContacts.Count == 0) Contacts.Text += "\nHead: N/A";

    else Contacts.Text += contactsText;


    if (supports.HasSupport) HasSupport.Text = "\nHas Support: TRUE";
    else HasSupport.Text = "\nHas Support: FALSE";


    var space = global.Player.Body.Space;
    //space.Solver.IterationLimit = 10;

    RawList<BroadPhaseOverlap> bpo = space.BroadPhase.Overlaps;
    string overlapText = "\n[color=#FF66FF]Space BP Overlaps: [/color]";

    for (int i = 0; i < bpo.Count; i++)
      overlapText += "\n[" + bpo[i].EntryA.Owner.GetType().Name + "] | [" + bpo[i].EntryB.Owner.GetType().Name + "]";
    Overlaps.Text = overlapText;
  }
} 