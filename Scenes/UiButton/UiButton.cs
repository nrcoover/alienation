using Godot;
using System;

public partial class UiButton : TextureButton
{
  [Export] private string _labelText;
  [Export] private Label _label;

  public override void _Ready() {
		_label.Text = _labelText;
  }
}
