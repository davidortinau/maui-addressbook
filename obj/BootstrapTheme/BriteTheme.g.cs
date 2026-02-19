// Auto-generated from CSS by MauiBootstrapTheme.Build. Do not edit.
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using MauiBootstrapTheme.Theming;
#pragma warning disable CS8604

namespace AddressBookPlus.Themes;

/// <summary>
/// Generated Bootstrap theme ResourceDictionary from CSS.
/// </summary>
public class BriteTheme : ResourceDictionary
{
    private static object DR(string key) => new Microsoft.Maui.Controls.Xaml.DynamicResourceExtension { Key = key }.ProvideValue(null!);

    public BriteTheme()
    {
        this["Primary"] = Color.FromArgb("#a2e436");
        this["Secondary"] = Color.FromArgb("#fff");
        this["Success"] = Color.FromArgb("#68d391");
        this["Danger"] = Color.FromArgb("#f56565");
        this["Warning"] = Color.FromArgb("#ffc700");
        this["Info"] = Color.FromArgb("#22d2ed");
        this["Light"] = Color.FromArgb("#e9ecef");
        this["Dark"] = Color.FromArgb("#000");

        this["OnPrimary"] = Color.FromArgb("#000");
        this["OnSecondary"] = Color.FromArgb("#000");
        this["OnSuccess"] = Color.FromArgb("#000");
        this["OnDanger"] = Color.FromArgb("#000");
        this["OnWarning"] = Color.FromArgb("#000");
        this["OnInfo"] = Color.FromArgb("#000");
        this["OnLight"] = Color.FromArgb("#000");
        this["OnDark"] = Color.FromArgb("#fff");

        this["BtnBorderPrimary"] = Color.FromArgb("#000");
        this["BtnBorderSecondary"] = Color.FromArgb("#000");
        this["BtnBorderSuccess"] = Color.FromArgb("#000");
        this["BtnBorderDanger"] = Color.FromArgb("#000");
        this["BtnBorderWarning"] = Color.FromArgb("#000");
        this["BtnBorderInfo"] = Color.FromArgb("#000");
        this["BtnBorderLight"] = Color.FromArgb("#000");
        this["BtnBorderDark"] = Color.FromArgb("#000");

        this["OutlineTextPrimary"] = Color.FromArgb("#000");
        this["OutlineBorderPrimary"] = Color.FromArgb("#000");
        this["OutlineTextSecondary"] = Color.FromArgb("#000");
        this["OutlineBorderSecondary"] = Color.FromArgb("#000");
        this["OutlineTextSuccess"] = Color.FromArgb("#000");
        this["OutlineBorderSuccess"] = Color.FromArgb("#000");
        this["OutlineTextDanger"] = Color.FromArgb("#000");
        this["OutlineBorderDanger"] = Color.FromArgb("#000");
        this["OutlineTextWarning"] = Color.FromArgb("#000");
        this["OutlineBorderWarning"] = Color.FromArgb("#000");
        this["OutlineTextInfo"] = Color.FromArgb("#000");
        this["OutlineBorderInfo"] = Color.FromArgb("#000");
        this["OutlineTextLight"] = Color.FromArgb("#000");
        this["OutlineBorderLight"] = Color.FromArgb("#000");
        this["OutlineTextDark"] = Color.FromArgb("#fff");
        this["OutlineBorderDark"] = Color.FromArgb("#000");

        this["Background"] = Color.FromArgb("#fff");
        this["Surface"] = Color.FromArgb("#fff");
        this["OnBackground"] = Color.FromArgb("#212529");
        this["OnSurface"] = Color.FromArgb("#212529");
        this["HeadingColor"] = Color.FromArgb("#212529");
        this["HeadingColorAlt"] = Color.FromArgb("#212529");
        this["Outline"] = Color.FromArgb("#000");
        this["OutlineVariant"] = Color.FromArgb("#2D000000");
        this["Muted"] = Color.FromArgb("#fff");

        // Dark mode override values stored as reference keys
        this["DarkBackground"] = Color.FromArgb("#212529");
        this["DarkOnBackground"] = Color.FromArgb("#dee2e6");
        this["DarkSurface"] = Color.FromArgb("#343a40");
        this["DarkOutline"] = Color.FromArgb("#495057");

        this["Gray100"] = Color.FromArgb("#f8f9fa");
        this["Gray200"] = Color.FromArgb("#e9ecef");
        this["Gray300"] = Color.FromArgb("#dee2e6");
        this["Gray400"] = Color.FromArgb("#ced4da");
        this["Gray500"] = Color.FromArgb("#adb5bd");
        this["Gray600"] = Color.FromArgb("#868e96");
        this["Gray700"] = Color.FromArgb("#495057");
        this["Gray800"] = Color.FromArgb("#343a40");
        this["Gray900"] = Color.FromArgb("#212529");

        this["FontFamily"] = "";
        this["FontSizeBase"] = 14d;
        this["FontSizeSm"] = 12.2d;
        this["FontSizeLg"] = 14d;
        this["FontSizeLead"] = 17.5d;
        this["FontSizeH1"] = 35d;
        this["FontSizeH2"] = 28d;
        this["FontSizeH3"] = 24.5d;
        this["FontSizeH4"] = 21d;
        this["FontSizeH5"] = 17.5d;
        this["FontSizeH6"] = 14d;

        this["CornerRadius"] = 6;
        this["CornerRadiusSm"] = 4;
        this["CornerRadiusLg"] = 8;
        this["BorderWidth"] = 2d;
        this["ButtonHeight"] = 41d;
        this["ButtonHeightSm"] = 30.299999999999997d;
        this["ButtonHeightLg"] = 49d;
        this["CornerRadiusPill"] = 21;
        this["CornerRadiusPillSm"] = 16;
        this["CornerRadiusPillLg"] = 25;
        this["InputHeight"] = 41d;
        this["InputHeightSm"] = 30.375d;
        this["InputHeightLg"] = 54.25d;
        this["ButtonPadding"] = new Thickness(16, 8);
        this["ButtonPaddingSm"] = new Thickness(12, 4);
        this["ButtonPaddingLg"] = new Thickness(20, 12);
        this["InputPadding"] = new Thickness(16, 8);
        this["ProgressBackground"] = Color.FromArgb("#e9ecef");
        this["InputBackground"] = Color.FromArgb("#fff");
        this["InputText"] = Color.FromArgb("#212529");
        this["PlaceholderColor"] = Color.FromArgb("#BF212529");
        // Button shadow resources
        this["BtnShadowBase"] = new Shadow { Brush = Color.FromArgb("#000"), Offset = new Point(3, 3), Radius = 0, Opacity = 1f };
        this["BtnShadowPrimary"] = new Shadow { Brush = Color.FromArgb("#000"), Offset = new Point(3, 3), Radius = 0, Opacity = 1f };
        this["BtnShadowOutlinePrimary"] = new Shadow { Brush = Colors.Transparent, Offset = new Point(0, 0), Radius = 0, Opacity = 0f };
        this["BtnShadowSecondary"] = new Shadow { Brush = Color.FromArgb("#000"), Offset = new Point(3, 3), Radius = 0, Opacity = 1f };
        this["BtnShadowOutlineSecondary"] = new Shadow { Brush = Colors.Transparent, Offset = new Point(0, 0), Radius = 0, Opacity = 0f };
        this["BtnShadowSuccess"] = new Shadow { Brush = Color.FromArgb("#000"), Offset = new Point(3, 3), Radius = 0, Opacity = 1f };
        this["BtnShadowOutlineSuccess"] = new Shadow { Brush = Colors.Transparent, Offset = new Point(0, 0), Radius = 0, Opacity = 0f };
        this["BtnShadowDanger"] = new Shadow { Brush = Color.FromArgb("#000"), Offset = new Point(3, 3), Radius = 0, Opacity = 1f };
        this["BtnShadowOutlineDanger"] = new Shadow { Brush = Colors.Transparent, Offset = new Point(0, 0), Radius = 0, Opacity = 0f };
        this["BtnShadowWarning"] = new Shadow { Brush = Color.FromArgb("#000"), Offset = new Point(3, 3), Radius = 0, Opacity = 1f };
        this["BtnShadowOutlineWarning"] = new Shadow { Brush = Colors.Transparent, Offset = new Point(0, 0), Radius = 0, Opacity = 0f };
        this["BtnShadowInfo"] = new Shadow { Brush = Color.FromArgb("#000"), Offset = new Point(3, 3), Radius = 0, Opacity = 1f };
        this["BtnShadowOutlineInfo"] = new Shadow { Brush = Colors.Transparent, Offset = new Point(0, 0), Radius = 0, Opacity = 0f };
        this["BtnShadowLight"] = new Shadow { Brush = Color.FromArgb("#000"), Offset = new Point(3, 3), Radius = 0, Opacity = 1f };
        this["BtnShadowOutlineLight"] = new Shadow { Brush = Colors.Transparent, Offset = new Point(0, 0), Radius = 0, Opacity = 0f };
        this["BtnShadowDark"] = new Shadow { Brush = Color.FromArgb("#000"), Offset = new Point(3, 3), Radius = 0, Opacity = 1f };
        this["BtnShadowOutlineDark"] = new Shadow { Brush = Colors.Transparent, Offset = new Point(0, 0), Radius = 0, Opacity = 0f };

        // Implicit styles
        var style_button = new Style(typeof(Button));
        style_button.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = DR("Primary") });
        style_button.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OnPrimary") });
        style_button.Setters.Add(new Setter { Property = Button.CornerRadiusProperty, Value = DR("CornerRadius") });
        style_button.Setters.Add(new Setter { Property = Button.FontFamilyProperty, Value = DR("FontFamily") });
        style_button.Setters.Add(new Setter { Property = Button.FontSizeProperty, Value = DR("FontSizeBase") });
        style_button.Setters.Add(new Setter { Property = Button.PaddingProperty, Value = DR("ButtonPadding") });
        style_button.Setters.Add(new Setter { Property = Button.MinimumHeightRequestProperty, Value = DR("ButtonHeight") });
        style_button.Setters.Add(new Setter { Property = Button.BorderWidthProperty, Value = DR("BorderWidth") });
        style_button.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = Colors.Transparent });
        style_button.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowBase") });
        Add(style_button);

        var style_label = new Style(typeof(Label));
        style_label.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("OnBackground") });
        style_label.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = DR("FontFamily") });
        style_label.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = DR("FontSizeBase") });
        Add(style_label);

        var style_form_label = new Style(typeof(Label)) { Class = "form-label" };
        style_form_label.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("OnBackground") });
        style_form_label.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = DR("FontSizeBase") });
        Add(style_form_label);

        var style_entry = new Style(typeof(Entry));
        style_entry.Setters.Add(new Setter { Property = Entry.TextColorProperty, Value = DR("InputText") });
        style_entry.Setters.Add(new Setter { Property = Entry.BackgroundProperty, Value = DR("InputBackground") });
        style_entry.Setters.Add(new Setter { Property = Entry.PlaceholderColorProperty, Value = DR("PlaceholderColor") });
        style_entry.Setters.Add(new Setter { Property = Entry.FontFamilyProperty, Value = DR("FontFamily") });
        style_entry.Setters.Add(new Setter { Property = Entry.FontSizeProperty, Value = DR("FontSizeBase") });
        style_entry.Setters.Add(new Setter { Property = Entry.MinimumHeightRequestProperty, Value = DR("InputHeight") });
        Add(style_entry);

        var style_form_control_entry = new Style(typeof(Entry)) { Class = "form-control" };
        style_form_control_entry.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Default });
        style_form_control_entry.Setters.Add(new Setter { Property = Entry.FontSizeProperty, Value = DR("FontSizeBase") });
        style_form_control_entry.Setters.Add(new Setter { Property = Entry.MinimumHeightRequestProperty, Value = DR("InputHeight") });
        Add(style_form_control_entry);

        var style_form_control_lg_entry = new Style(typeof(Entry)) { Class = "form-control-lg" };
        style_form_control_lg_entry.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Large });
        style_form_control_lg_entry.Setters.Add(new Setter { Property = Entry.FontSizeProperty, Value = DR("FontSizeLg") });
        style_form_control_lg_entry.Setters.Add(new Setter { Property = Entry.MinimumHeightRequestProperty, Value = DR("InputHeightLg") });
        Add(style_form_control_lg_entry);

        var style_form_control_sm_entry = new Style(typeof(Entry)) { Class = "form-control-sm" };
        style_form_control_sm_entry.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Small });
        style_form_control_sm_entry.Setters.Add(new Setter { Property = Entry.FontSizeProperty, Value = DR("FontSizeSm") });
        style_form_control_sm_entry.Setters.Add(new Setter { Property = Entry.MinimumHeightRequestProperty, Value = DR("InputHeightSm") });
        Add(style_form_control_sm_entry);

        var style_picker = new Style(typeof(Picker));
        style_picker.Setters.Add(new Setter { Property = Picker.TextColorProperty, Value = DR("InputText") });
        style_picker.Setters.Add(new Setter { Property = Picker.TitleColorProperty, Value = DR("PlaceholderColor") });
        style_picker.Setters.Add(new Setter { Property = Picker.FontFamilyProperty, Value = DR("FontFamily") });
        style_picker.Setters.Add(new Setter { Property = Picker.FontSizeProperty, Value = DR("FontSizeBase") });
        style_picker.Setters.Add(new Setter { Property = Picker.MinimumHeightRequestProperty, Value = DR("InputHeight") });
        Add(style_picker);

        var style_form_select_picker = new Style(typeof(Picker)) { Class = "form-select" };
        style_form_select_picker.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Default });
        style_form_select_picker.Setters.Add(new Setter { Property = Picker.FontSizeProperty, Value = DR("FontSizeBase") });
        style_form_select_picker.Setters.Add(new Setter { Property = Picker.MinimumHeightRequestProperty, Value = DR("InputHeight") });
        Add(style_form_select_picker);

        var style_form_select_lg_picker = new Style(typeof(Picker)) { Class = "form-select-lg" };
        style_form_select_lg_picker.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Large });
        style_form_select_lg_picker.Setters.Add(new Setter { Property = Picker.FontSizeProperty, Value = DR("FontSizeLg") });
        style_form_select_lg_picker.Setters.Add(new Setter { Property = Picker.MinimumHeightRequestProperty, Value = DR("InputHeightLg") });
        Add(style_form_select_lg_picker);

        var style_form_select_sm_picker = new Style(typeof(Picker)) { Class = "form-select-sm" };
        style_form_select_sm_picker.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Small });
        style_form_select_sm_picker.Setters.Add(new Setter { Property = Picker.FontSizeProperty, Value = DR("FontSizeSm") });
        style_form_select_sm_picker.Setters.Add(new Setter { Property = Picker.MinimumHeightRequestProperty, Value = DR("InputHeightSm") });
        Add(style_form_select_sm_picker);

        var style_datepicker = new Style(typeof(DatePicker));
        style_datepicker.Setters.Add(new Setter { Property = DatePicker.TextColorProperty, Value = DR("InputText") });
        style_datepicker.Setters.Add(new Setter { Property = DatePicker.FontFamilyProperty, Value = DR("FontFamily") });
        style_datepicker.Setters.Add(new Setter { Property = DatePicker.FontSizeProperty, Value = DR("FontSizeBase") });
        style_datepicker.Setters.Add(new Setter { Property = DatePicker.MinimumHeightRequestProperty, Value = DR("InputHeight") });
        Add(style_datepicker);

        var style_form_control_datepicker = new Style(typeof(DatePicker)) { Class = "form-control" };
        style_form_control_datepicker.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Default });
        style_form_control_datepicker.Setters.Add(new Setter { Property = DatePicker.FontSizeProperty, Value = DR("FontSizeBase") });
        style_form_control_datepicker.Setters.Add(new Setter { Property = DatePicker.MinimumHeightRequestProperty, Value = DR("InputHeight") });
        Add(style_form_control_datepicker);

        var style_form_control_lg_datepicker = new Style(typeof(DatePicker)) { Class = "form-control-lg" };
        style_form_control_lg_datepicker.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Large });
        style_form_control_lg_datepicker.Setters.Add(new Setter { Property = DatePicker.FontSizeProperty, Value = DR("FontSizeLg") });
        style_form_control_lg_datepicker.Setters.Add(new Setter { Property = DatePicker.MinimumHeightRequestProperty, Value = DR("InputHeightLg") });
        Add(style_form_control_lg_datepicker);

        var style_form_control_sm_datepicker = new Style(typeof(DatePicker)) { Class = "form-control-sm" };
        style_form_control_sm_datepicker.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Small });
        style_form_control_sm_datepicker.Setters.Add(new Setter { Property = DatePicker.FontSizeProperty, Value = DR("FontSizeSm") });
        style_form_control_sm_datepicker.Setters.Add(new Setter { Property = DatePicker.MinimumHeightRequestProperty, Value = DR("InputHeightSm") });
        Add(style_form_control_sm_datepicker);

        var style_timepicker = new Style(typeof(TimePicker));
        style_timepicker.Setters.Add(new Setter { Property = TimePicker.TextColorProperty, Value = DR("InputText") });
        style_timepicker.Setters.Add(new Setter { Property = TimePicker.FontFamilyProperty, Value = DR("FontFamily") });
        style_timepicker.Setters.Add(new Setter { Property = TimePicker.FontSizeProperty, Value = DR("FontSizeBase") });
        style_timepicker.Setters.Add(new Setter { Property = TimePicker.MinimumHeightRequestProperty, Value = DR("InputHeight") });
        Add(style_timepicker);

        var style_form_control_timepicker = new Style(typeof(TimePicker)) { Class = "form-control" };
        style_form_control_timepicker.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Default });
        style_form_control_timepicker.Setters.Add(new Setter { Property = TimePicker.FontSizeProperty, Value = DR("FontSizeBase") });
        style_form_control_timepicker.Setters.Add(new Setter { Property = TimePicker.MinimumHeightRequestProperty, Value = DR("InputHeight") });
        Add(style_form_control_timepicker);

        var style_form_control_lg_timepicker = new Style(typeof(TimePicker)) { Class = "form-control-lg" };
        style_form_control_lg_timepicker.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Large });
        style_form_control_lg_timepicker.Setters.Add(new Setter { Property = TimePicker.FontSizeProperty, Value = DR("FontSizeLg") });
        style_form_control_lg_timepicker.Setters.Add(new Setter { Property = TimePicker.MinimumHeightRequestProperty, Value = DR("InputHeightLg") });
        Add(style_form_control_lg_timepicker);

        var style_form_control_sm_timepicker = new Style(typeof(TimePicker)) { Class = "form-control-sm" };
        style_form_control_sm_timepicker.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Small });
        style_form_control_sm_timepicker.Setters.Add(new Setter { Property = TimePicker.FontSizeProperty, Value = DR("FontSizeSm") });
        style_form_control_sm_timepicker.Setters.Add(new Setter { Property = TimePicker.MinimumHeightRequestProperty, Value = DR("InputHeightSm") });
        Add(style_form_control_sm_timepicker);

        var style_editor = new Style(typeof(Editor));
        style_editor.Setters.Add(new Setter { Property = Editor.TextColorProperty, Value = DR("InputText") });
        style_editor.Setters.Add(new Setter { Property = Editor.BackgroundProperty, Value = DR("InputBackground") });
        style_editor.Setters.Add(new Setter { Property = Editor.FontFamilyProperty, Value = DR("FontFamily") });
        style_editor.Setters.Add(new Setter { Property = Editor.FontSizeProperty, Value = DR("FontSizeBase") });
        style_editor.Setters.Add(new Setter { Property = Editor.MinimumHeightRequestProperty, Value = DR("InputHeight") });
        Add(style_editor);

        var style_form_control_editor = new Style(typeof(Editor)) { Class = "form-control" };
        style_form_control_editor.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Default });
        style_form_control_editor.Setters.Add(new Setter { Property = Editor.FontSizeProperty, Value = DR("FontSizeBase") });
        style_form_control_editor.Setters.Add(new Setter { Property = Editor.MinimumHeightRequestProperty, Value = DR("InputHeight") });
        Add(style_form_control_editor);

        var style_form_control_lg_editor = new Style(typeof(Editor)) { Class = "form-control-lg" };
        style_form_control_lg_editor.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Large });
        style_form_control_lg_editor.Setters.Add(new Setter { Property = Editor.FontSizeProperty, Value = DR("FontSizeLg") });
        style_form_control_lg_editor.Setters.Add(new Setter { Property = Editor.MinimumHeightRequestProperty, Value = DR("InputHeightLg") });
        Add(style_form_control_lg_editor);

        var style_form_control_sm_editor = new Style(typeof(Editor)) { Class = "form-control-sm" };
        style_form_control_sm_editor.Setters.Add(new Setter { Property = Bootstrap.SizeProperty, Value = BootstrapSize.Small });
        style_form_control_sm_editor.Setters.Add(new Setter { Property = Editor.FontSizeProperty, Value = DR("FontSizeSm") });
        style_form_control_sm_editor.Setters.Add(new Setter { Property = Editor.MinimumHeightRequestProperty, Value = DR("InputHeightSm") });
        Add(style_form_control_sm_editor);

        var style_form_check_checkbox = new Style(typeof(CheckBox)) { Class = "form-check-input" };
        style_form_check_checkbox.Setters.Add(new Setter { Property = CheckBox.ColorProperty, Value = DR("Primary") });
        Add(style_form_check_checkbox);

        var style_form_check_radio = new Style(typeof(RadioButton)) { Class = "form-check-input" };
        style_form_check_radio.Setters.Add(new Setter { Property = RadioButton.TextColorProperty, Value = DR("OnBackground") });
        Add(style_form_check_radio);

        var style_form_switch = new Style(typeof(Switch)) { Class = "form-switch" };
        style_form_switch.Setters.Add(new Setter { Property = Switch.OnColorProperty, Value = DR("Primary") });
        Add(style_form_switch);

        var style_form_range = new Style(typeof(Slider)) { Class = "form-range" };
        style_form_range.Setters.Add(new Setter { Property = Slider.MinimumTrackColorProperty, Value = DR("Primary") });
        style_form_range.Setters.Add(new Setter { Property = Slider.ThumbColorProperty, Value = DR("Primary") });
        style_form_range.Setters.Add(new Setter { Property = Slider.MaximumTrackColorProperty, Value = DR("OutlineVariant") });
        Add(style_form_range);

        var style_progressbar = new Style(typeof(ProgressBar));
        style_progressbar.Setters.Add(new Setter { Property = ProgressBar.ProgressColorProperty, Value = DR("Primary") });
        style_progressbar.Setters.Add(new Setter { Property = ProgressBar.MinimumHeightRequestProperty, Value = 16d });
        Add(style_progressbar);

        var style_border = new Style(typeof(Border));
        style_border.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = Colors.Transparent });
        style_border.Setters.Add(new Setter { Property = Border.StrokeProperty, Value = Colors.Transparent });
        style_border.Setters.Add(new Setter { Property = Border.StrokeThicknessProperty, Value = 0d });
        Add(style_border);

        var style_page = new Style(typeof(ContentPage)) { ApplyToDerivedTypes = true };
        style_page.Setters.Add(new Setter { Property = ContentPage.BackgroundProperty, Value = DR("Background") });
        Add(style_page);

        var style_shell = new Style(typeof(Shell)) { ApplyToDerivedTypes = true };
        style_shell.Setters.Add(new Setter { Property = Shell.BackgroundColorProperty, Value = DR("Primary") });
        style_shell.Setters.Add(new Setter { Property = Shell.ForegroundColorProperty, Value = DR("OnPrimary") });
        style_shell.Setters.Add(new Setter { Property = Shell.TitleColorProperty, Value = DR("OnPrimary") });
        style_shell.Setters.Add(new Setter { Property = Shell.TabBarBackgroundColorProperty, Value = DR("Background") });
        style_shell.Setters.Add(new Setter { Property = Shell.TabBarForegroundColorProperty, Value = DR("Primary") });
        style_shell.Setters.Add(new Setter { Property = Shell.TabBarUnselectedColorProperty, Value = DR("Gray500") });
        style_shell.Setters.Add(new Setter { Property = Shell.FlyoutBackgroundColorProperty, Value = DR("Background") });
        Add(style_shell);

        // Button variant styles
        var style_btn_primary = new Style(typeof(Button)) { Class = "btn-primary" };
        style_btn_primary.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = DR("Primary") });
        style_btn_primary.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OnPrimary") });
        style_btn_primary.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("BtnBorderPrimary") });
        style_btn_primary.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowPrimary") });
        Add(style_btn_primary);

        var style_btn_secondary = new Style(typeof(Button)) { Class = "btn-secondary" };
        style_btn_secondary.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = DR("Secondary") });
        style_btn_secondary.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OnSecondary") });
        style_btn_secondary.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("BtnBorderSecondary") });
        style_btn_secondary.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowSecondary") });
        Add(style_btn_secondary);

        var style_btn_success = new Style(typeof(Button)) { Class = "btn-success" };
        style_btn_success.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = DR("Success") });
        style_btn_success.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OnSuccess") });
        style_btn_success.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("BtnBorderSuccess") });
        style_btn_success.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowSuccess") });
        Add(style_btn_success);

        var style_btn_danger = new Style(typeof(Button)) { Class = "btn-danger" };
        style_btn_danger.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = DR("Danger") });
        style_btn_danger.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OnDanger") });
        style_btn_danger.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("BtnBorderDanger") });
        style_btn_danger.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowDanger") });
        Add(style_btn_danger);

        var style_btn_warning = new Style(typeof(Button)) { Class = "btn-warning" };
        style_btn_warning.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = DR("Warning") });
        style_btn_warning.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OnWarning") });
        style_btn_warning.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("BtnBorderWarning") });
        style_btn_warning.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowWarning") });
        Add(style_btn_warning);

        var style_btn_info = new Style(typeof(Button)) { Class = "btn-info" };
        style_btn_info.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = DR("Info") });
        style_btn_info.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OnInfo") });
        style_btn_info.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("BtnBorderInfo") });
        style_btn_info.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowInfo") });
        Add(style_btn_info);

        var style_btn_light = new Style(typeof(Button)) { Class = "btn-light" };
        style_btn_light.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = DR("Light") });
        style_btn_light.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OnLight") });
        style_btn_light.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("BtnBorderLight") });
        style_btn_light.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowLight") });
        Add(style_btn_light);

        var style_btn_dark = new Style(typeof(Button)) { Class = "btn-dark" };
        style_btn_dark.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = DR("Dark") });
        style_btn_dark.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OnDark") });
        style_btn_dark.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("BtnBorderDark") });
        style_btn_dark.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowDark") });
        Add(style_btn_dark);

        // Outline button variants
        var style_btnout_primary = new Style(typeof(Button)) { Class = "btn-outline-primary" };
        style_btnout_primary.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = Colors.Transparent });
        style_btnout_primary.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OutlineTextPrimary") });
        style_btnout_primary.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("OutlineBorderPrimary") });
        style_btnout_primary.Setters.Add(new Setter { Property = Button.BorderWidthProperty, Value = DR("BorderWidth") });
        style_btnout_primary.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowOutlinePrimary") });
        Add(style_btnout_primary);

        var style_btnout_secondary = new Style(typeof(Button)) { Class = "btn-outline-secondary" };
        style_btnout_secondary.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = Colors.Transparent });
        style_btnout_secondary.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OutlineTextSecondary") });
        style_btnout_secondary.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("OutlineBorderSecondary") });
        style_btnout_secondary.Setters.Add(new Setter { Property = Button.BorderWidthProperty, Value = DR("BorderWidth") });
        style_btnout_secondary.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowOutlineSecondary") });
        Add(style_btnout_secondary);

        var style_btnout_success = new Style(typeof(Button)) { Class = "btn-outline-success" };
        style_btnout_success.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = Colors.Transparent });
        style_btnout_success.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OutlineTextSuccess") });
        style_btnout_success.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("OutlineBorderSuccess") });
        style_btnout_success.Setters.Add(new Setter { Property = Button.BorderWidthProperty, Value = DR("BorderWidth") });
        style_btnout_success.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowOutlineSuccess") });
        Add(style_btnout_success);

        var style_btnout_danger = new Style(typeof(Button)) { Class = "btn-outline-danger" };
        style_btnout_danger.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = Colors.Transparent });
        style_btnout_danger.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OutlineTextDanger") });
        style_btnout_danger.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("OutlineBorderDanger") });
        style_btnout_danger.Setters.Add(new Setter { Property = Button.BorderWidthProperty, Value = DR("BorderWidth") });
        style_btnout_danger.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowOutlineDanger") });
        Add(style_btnout_danger);

        var style_btnout_warning = new Style(typeof(Button)) { Class = "btn-outline-warning" };
        style_btnout_warning.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = Colors.Transparent });
        style_btnout_warning.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OutlineTextWarning") });
        style_btnout_warning.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("OutlineBorderWarning") });
        style_btnout_warning.Setters.Add(new Setter { Property = Button.BorderWidthProperty, Value = DR("BorderWidth") });
        style_btnout_warning.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowOutlineWarning") });
        Add(style_btnout_warning);

        var style_btnout_info = new Style(typeof(Button)) { Class = "btn-outline-info" };
        style_btnout_info.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = Colors.Transparent });
        style_btnout_info.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OutlineTextInfo") });
        style_btnout_info.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("OutlineBorderInfo") });
        style_btnout_info.Setters.Add(new Setter { Property = Button.BorderWidthProperty, Value = DR("BorderWidth") });
        style_btnout_info.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowOutlineInfo") });
        Add(style_btnout_info);

        var style_btnout_light = new Style(typeof(Button)) { Class = "btn-outline-light" };
        style_btnout_light.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = Colors.Transparent });
        style_btnout_light.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OutlineTextLight") });
        style_btnout_light.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("OutlineBorderLight") });
        style_btnout_light.Setters.Add(new Setter { Property = Button.BorderWidthProperty, Value = DR("BorderWidth") });
        style_btnout_light.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowOutlineLight") });
        Add(style_btnout_light);

        var style_btnout_dark = new Style(typeof(Button)) { Class = "btn-outline-dark" };
        style_btnout_dark.Setters.Add(new Setter { Property = Button.BackgroundProperty, Value = Colors.Transparent });
        style_btnout_dark.Setters.Add(new Setter { Property = Button.TextColorProperty, Value = DR("OutlineTextDark") });
        style_btnout_dark.Setters.Add(new Setter { Property = Button.BorderColorProperty, Value = DR("OutlineBorderDark") });
        style_btnout_dark.Setters.Add(new Setter { Property = Button.BorderWidthProperty, Value = DR("BorderWidth") });
        style_btnout_dark.Setters.Add(new Setter { Property = Button.ShadowProperty, Value = DR("BtnShadowOutlineDark") });
        Add(style_btnout_dark);

        // Size variants
        var style_btn_lg = new Style(typeof(Button)) { Class = "btn-lg" };
        style_btn_lg.Setters.Add(new Setter { Property = Button.FontSizeProperty, Value = DR("FontSizeLg") });
        style_btn_lg.Setters.Add(new Setter { Property = Button.PaddingProperty, Value = DR("ButtonPaddingLg") });
        style_btn_lg.Setters.Add(new Setter { Property = Button.MinimumHeightRequestProperty, Value = DR("ButtonHeightLg") });
        style_btn_lg.Setters.Add(new Setter { Property = Button.CornerRadiusProperty, Value = DR("CornerRadiusLg") });
        Add(style_btn_lg);

        var style_btn_sm = new Style(typeof(Button)) { Class = "btn-sm" };
        style_btn_sm.Setters.Add(new Setter { Property = Button.FontSizeProperty, Value = DR("FontSizeSm") });
        style_btn_sm.Setters.Add(new Setter { Property = Button.PaddingProperty, Value = DR("ButtonPaddingSm") });
        style_btn_sm.Setters.Add(new Setter { Property = Button.MinimumHeightRequestProperty, Value = DR("ButtonHeightSm") });
        style_btn_sm.Setters.Add(new Setter { Property = Button.CornerRadiusProperty, Value = DR("CornerRadiusSm") });
        Add(style_btn_sm);

        var style_btn_pill = new Style(typeof(Button)) { Class = "btn-pill" };
        style_btn_pill.Setters.Add(new Setter { Property = Button.CornerRadiusProperty, Value = DR("CornerRadiusPill") });
        Add(style_btn_pill);

        var style_btn_pill_lg = new Style(typeof(Button)) { Class = "btn-pill-lg" };
        style_btn_pill_lg.Setters.Add(new Setter { Property = Button.FontSizeProperty, Value = DR("FontSizeLg") });
        style_btn_pill_lg.Setters.Add(new Setter { Property = Button.PaddingProperty, Value = DR("ButtonPaddingLg") });
        style_btn_pill_lg.Setters.Add(new Setter { Property = Button.MinimumHeightRequestProperty, Value = DR("ButtonHeightLg") });
        style_btn_pill_lg.Setters.Add(new Setter { Property = Button.CornerRadiusProperty, Value = DR("CornerRadiusPillLg") });
        Add(style_btn_pill_lg);

        var style_btn_pill_sm = new Style(typeof(Button)) { Class = "btn-pill-sm" };
        style_btn_pill_sm.Setters.Add(new Setter { Property = Button.FontSizeProperty, Value = DR("FontSizeSm") });
        style_btn_pill_sm.Setters.Add(new Setter { Property = Button.PaddingProperty, Value = DR("ButtonPaddingSm") });
        style_btn_pill_sm.Setters.Add(new Setter { Property = Button.MinimumHeightRequestProperty, Value = DR("ButtonHeightSm") });
        style_btn_pill_sm.Setters.Add(new Setter { Property = Button.CornerRadiusProperty, Value = DR("CornerRadiusPillSm") });
        Add(style_btn_pill_sm);

        // Card & component styles
        var style_card = new Style(typeof(Border)) { Class = "card" };
        style_card.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Surface") });
        style_card.Setters.Add(new Setter { Property = Border.StrokeProperty, Value = DR("Outline") });
        style_card.Setters.Add(new Setter { Property = Border.StrokeThicknessProperty, Value = DR("BorderWidth") });
        style_card.Setters.Add(new Setter { Property = Border.StrokeShapeProperty, Value = new RoundRectangle { CornerRadius = 6 } });
        style_card.Setters.Add(new Setter { Property = Border.PaddingProperty, Value = new Thickness(16, 16) });
        Add(style_card);

        var style_shadow = new Style(typeof(VisualElement)) { Class = "shadow", ApplyToDerivedTypes = true };
        style_shadow.Setters.Add(new Setter { Property = VisualElement.ShadowProperty, Value = new Shadow { Brush = Colors.Black, Offset = new Point(0, 8), Radius = 16, Opacity = 0.15f } });
        Add(style_shadow);

        var style_shadow_sm = new Style(typeof(VisualElement)) { Class = "shadow-sm", ApplyToDerivedTypes = true };
        style_shadow_sm.Setters.Add(new Setter { Property = VisualElement.ShadowProperty, Value = new Shadow { Brush = Colors.Black, Offset = new Point(0, 2), Radius = 4, Opacity = 0.075f } });
        Add(style_shadow_sm);

        var style_shadow_lg = new Style(typeof(VisualElement)) { Class = "shadow-lg", ApplyToDerivedTypes = true };
        style_shadow_lg.Setters.Add(new Setter { Property = VisualElement.ShadowProperty, Value = new Shadow { Brush = Colors.Black, Offset = new Point(0, 16), Radius = 48, Opacity = 0.175f } });
        Add(style_shadow_lg);

        var style_badge = new Style(typeof(Border)) { Class = "badge" };
        style_badge.Setters.Add(new Setter { Property = Border.StrokeThicknessProperty, Value = 0d });
        style_badge.Setters.Add(new Setter { Property = Border.StrokeShapeProperty, Value = new RoundRectangle { CornerRadius = 6 } });
        style_badge.Setters.Add(new Setter { Property = Border.PaddingProperty, Value = new Thickness(8, 4) });
        Add(style_badge);

        // Color variant cards
        var style_textbg_primary = new Style(typeof(Border)) { Class = "text-bg-primary" };
        style_textbg_primary.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Primary") });
        style_textbg_primary.Setters.Add(new Setter { Property = Border.StrokeThicknessProperty, Value = 0d });
        style_textbg_primary.Setters.Add(new Setter { Property = Border.StrokeShapeProperty, Value = new RoundRectangle { CornerRadius = 6 } });
        style_textbg_primary.Setters.Add(new Setter { Property = Border.PaddingProperty, Value = new Thickness(16, 16) });
        Add(style_textbg_primary);
        var style_textbg_secondary = new Style(typeof(Border)) { Class = "text-bg-secondary" };
        style_textbg_secondary.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Secondary") });
        style_textbg_secondary.Setters.Add(new Setter { Property = Border.StrokeThicknessProperty, Value = 0d });
        style_textbg_secondary.Setters.Add(new Setter { Property = Border.StrokeShapeProperty, Value = new RoundRectangle { CornerRadius = 6 } });
        style_textbg_secondary.Setters.Add(new Setter { Property = Border.PaddingProperty, Value = new Thickness(16, 16) });
        Add(style_textbg_secondary);
        var style_textbg_success = new Style(typeof(Border)) { Class = "text-bg-success" };
        style_textbg_success.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Success") });
        style_textbg_success.Setters.Add(new Setter { Property = Border.StrokeThicknessProperty, Value = 0d });
        style_textbg_success.Setters.Add(new Setter { Property = Border.StrokeShapeProperty, Value = new RoundRectangle { CornerRadius = 6 } });
        style_textbg_success.Setters.Add(new Setter { Property = Border.PaddingProperty, Value = new Thickness(16, 16) });
        Add(style_textbg_success);
        var style_textbg_danger = new Style(typeof(Border)) { Class = "text-bg-danger" };
        style_textbg_danger.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Danger") });
        style_textbg_danger.Setters.Add(new Setter { Property = Border.StrokeThicknessProperty, Value = 0d });
        style_textbg_danger.Setters.Add(new Setter { Property = Border.StrokeShapeProperty, Value = new RoundRectangle { CornerRadius = 6 } });
        style_textbg_danger.Setters.Add(new Setter { Property = Border.PaddingProperty, Value = new Thickness(16, 16) });
        Add(style_textbg_danger);
        var style_textbg_warning = new Style(typeof(Border)) { Class = "text-bg-warning" };
        style_textbg_warning.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Warning") });
        style_textbg_warning.Setters.Add(new Setter { Property = Border.StrokeThicknessProperty, Value = 0d });
        style_textbg_warning.Setters.Add(new Setter { Property = Border.StrokeShapeProperty, Value = new RoundRectangle { CornerRadius = 6 } });
        style_textbg_warning.Setters.Add(new Setter { Property = Border.PaddingProperty, Value = new Thickness(16, 16) });
        Add(style_textbg_warning);
        var style_textbg_info = new Style(typeof(Border)) { Class = "text-bg-info" };
        style_textbg_info.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Info") });
        style_textbg_info.Setters.Add(new Setter { Property = Border.StrokeThicknessProperty, Value = 0d });
        style_textbg_info.Setters.Add(new Setter { Property = Border.StrokeShapeProperty, Value = new RoundRectangle { CornerRadius = 6 } });
        style_textbg_info.Setters.Add(new Setter { Property = Border.PaddingProperty, Value = new Thickness(16, 16) });
        Add(style_textbg_info);
        var style_textbg_light = new Style(typeof(Border)) { Class = "text-bg-light" };
        style_textbg_light.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Light") });
        style_textbg_light.Setters.Add(new Setter { Property = Border.StrokeThicknessProperty, Value = 0d });
        style_textbg_light.Setters.Add(new Setter { Property = Border.StrokeShapeProperty, Value = new RoundRectangle { CornerRadius = 6 } });
        style_textbg_light.Setters.Add(new Setter { Property = Border.PaddingProperty, Value = new Thickness(16, 16) });
        Add(style_textbg_light);
        var style_textbg_dark = new Style(typeof(Border)) { Class = "text-bg-dark" };
        style_textbg_dark.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Dark") });
        style_textbg_dark.Setters.Add(new Setter { Property = Border.StrokeThicknessProperty, Value = 0d });
        style_textbg_dark.Setters.Add(new Setter { Property = Border.StrokeShapeProperty, Value = new RoundRectangle { CornerRadius = 6 } });
        style_textbg_dark.Setters.Add(new Setter { Property = Border.PaddingProperty, Value = new Thickness(16, 16) });
        Add(style_textbg_dark);

        // Badge backgrounds
        var style_bg_primary = new Style(typeof(Border)) { Class = "bg-primary" };
        style_bg_primary.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Primary") });
        Add(style_bg_primary);
        var style_bg_secondary = new Style(typeof(Border)) { Class = "bg-secondary" };
        style_bg_secondary.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Secondary") });
        Add(style_bg_secondary);
        var style_bg_success = new Style(typeof(Border)) { Class = "bg-success" };
        style_bg_success.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Success") });
        Add(style_bg_success);
        var style_bg_danger = new Style(typeof(Border)) { Class = "bg-danger" };
        style_bg_danger.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Danger") });
        Add(style_bg_danger);
        var style_bg_warning = new Style(typeof(Border)) { Class = "bg-warning" };
        style_bg_warning.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Warning") });
        Add(style_bg_warning);
        var style_bg_info = new Style(typeof(Border)) { Class = "bg-info" };
        style_bg_info.Setters.Add(new Setter { Property = Border.BackgroundProperty, Value = DR("Info") });
        Add(style_bg_info);

        // On-color label styles
        var style_on_primary = new Style(typeof(Label)) { Class = "on-primary" };
        style_on_primary.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("OnPrimary") });
        Add(style_on_primary);
        var style_on_secondary = new Style(typeof(Label)) { Class = "on-secondary" };
        style_on_secondary.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("OnSecondary") });
        Add(style_on_secondary);
        var style_on_success = new Style(typeof(Label)) { Class = "on-success" };
        style_on_success.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("OnSuccess") });
        Add(style_on_success);
        var style_on_danger = new Style(typeof(Label)) { Class = "on-danger" };
        style_on_danger.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("OnDanger") });
        Add(style_on_danger);
        var style_on_warning = new Style(typeof(Label)) { Class = "on-warning" };
        style_on_warning.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("OnWarning") });
        Add(style_on_warning);
        var style_on_info = new Style(typeof(Label)) { Class = "on-info" };
        style_on_info.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("OnInfo") });
        Add(style_on_info);
        var style_on_light = new Style(typeof(Label)) { Class = "on-light" };
        style_on_light.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("OnLight") });
        Add(style_on_light);
        var style_on_dark = new Style(typeof(Label)) { Class = "on-dark" };
        style_on_dark.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("OnDark") });
        Add(style_on_dark);

        // Heading styles
        var style_h1 = new Style(typeof(Label)) { Class = "h1" };
        style_h1.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = DR("FontSizeH1") });
        style_h1.Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold });
        style_h1.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("HeadingColor") });
        Add(style_h1);

        var style_h2 = new Style(typeof(Label)) { Class = "h2" };
        style_h2.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = DR("FontSizeH2") });
        style_h2.Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold });
        style_h2.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("HeadingColorAlt") });
        Add(style_h2);

        var style_h3 = new Style(typeof(Label)) { Class = "h3" };
        style_h3.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = DR("FontSizeH3") });
        style_h3.Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold });
        style_h3.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("HeadingColorAlt") });
        Add(style_h3);

        var style_h4 = new Style(typeof(Label)) { Class = "h4" };
        style_h4.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = DR("FontSizeH4") });
        style_h4.Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold });
        style_h4.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("HeadingColorAlt") });
        Add(style_h4);

        var style_h5 = new Style(typeof(Label)) { Class = "h5" };
        style_h5.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = DR("FontSizeH5") });
        style_h5.Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold });
        style_h5.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("HeadingColorAlt") });
        Add(style_h5);

        var style_h6 = new Style(typeof(Label)) { Class = "h6" };
        style_h6.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = DR("FontSizeH6") });
        style_h6.Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold });
        style_h6.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("HeadingColorAlt") });
        Add(style_h6);

        // Text styles
        var style_lead = new Style(typeof(Label)) { Class = "lead" };
        style_lead.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = DR("FontSizeLead") });
        style_lead.Setters.Add(new Setter { Property = Label.LineHeightProperty, Value = 1.5 });
        Add(style_lead);
        var style_small = new Style(typeof(Label)) { Class = "small" };
        style_small.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = DR("FontSizeSm") });
        Add(style_small);
        var style_form_text = new Style(typeof(Label)) { Class = "form-text" };
        style_form_text.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = DR("FontSizeSm") });
        style_form_text.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("Muted") });
        Add(style_form_text);
        var style_form_check_label = new Style(typeof(Label)) { Class = "form-check-label" };
        style_form_check_label.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("OnBackground") });
        Add(style_form_check_label);
        var style_mark = new Style(typeof(Label)) { Class = "mark" };
        style_mark.Setters.Add(new Setter { Property = Label.BackgroundProperty, Value = Color.FromArgb("#fcf8e3") });
        style_mark.Setters.Add(new Setter { Property = Label.PaddingProperty, Value = new Thickness(4, 2) });
        Add(style_mark);
        var style_text_primary = new Style(typeof(Label)) { Class = "text-primary" };
        style_text_primary.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("Primary") });
        Add(style_text_primary);
        var style_text_secondary = new Style(typeof(Label)) { Class = "text-secondary" };
        style_text_secondary.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("Secondary") });
        Add(style_text_secondary);
        var style_text_success = new Style(typeof(Label)) { Class = "text-success" };
        style_text_success.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("Success") });
        Add(style_text_success);
        var style_text_danger = new Style(typeof(Label)) { Class = "text-danger" };
        style_text_danger.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("Danger") });
        Add(style_text_danger);
        var style_text_warning = new Style(typeof(Label)) { Class = "text-warning" };
        style_text_warning.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("Warning") });
        Add(style_text_warning);
        var style_text_info = new Style(typeof(Label)) { Class = "text-info" };
        style_text_info.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("Info") });
        Add(style_text_info);
        var style_text_muted = new Style(typeof(Label)) { Class = "text-muted" };
        style_text_muted.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("Muted") });
        Add(style_text_muted);
        var style_text_dark = new Style(typeof(Label)) { Class = "text-dark" };
        style_text_dark.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = DR("Dark") });
        Add(style_text_dark);
        var style_text_white = new Style(typeof(Label)) { Class = "text-white" };
        style_text_white.Setters.Add(new Setter { Property = Label.TextColorProperty, Value = Colors.White });
        Add(style_text_white);
        var style_text_center = new Style(typeof(Label)) { Class = "text-center" };
        style_text_center.Setters.Add(new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center });
        Add(style_text_center);

        // Progress bar variants
        var style_progress_primary = new Style(typeof(ProgressBar)) { Class = "progress-primary" };
        style_progress_primary.Setters.Add(new Setter { Property = ProgressBar.ProgressColorProperty, Value = DR("Primary") });
        Add(style_progress_primary);
        var style_progress_success = new Style(typeof(ProgressBar)) { Class = "progress-success" };
        style_progress_success.Setters.Add(new Setter { Property = ProgressBar.ProgressColorProperty, Value = DR("Success") });
        Add(style_progress_success);
        var style_progress_danger = new Style(typeof(ProgressBar)) { Class = "progress-danger" };
        style_progress_danger.Setters.Add(new Setter { Property = ProgressBar.ProgressColorProperty, Value = DR("Danger") });
        Add(style_progress_danger);


        // Apply initial theme mode based on current system theme
        if (Application.Current != null)
        {
            ApplyThemeMode(Application.Current.RequestedTheme);
            var weakSelf = new WeakReference<BriteTheme>(this);
            Application.Current.RequestedThemeChanged += (s, e) =>
            {
                if (weakSelf.TryGetTarget(out var self))
                    self.ApplyThemeMode(e.RequestedTheme);
            };
        }
    }

    /// <summary>
    /// Applies light or dark mode color overrides from the CSS [data-bs-theme=dark] block.
    /// </summary>
    private void ApplyThemeMode(AppTheme theme)
    {
        if (!Microsoft.Maui.ApplicationModel.MainThread.IsMainThread)
        {
            Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() => ApplyThemeMode(theme));
            return;
        }

        if (theme == AppTheme.Dark)
        {
            this["Background"] = Color.FromArgb("#212529");
            this["OnBackground"] = Color.FromArgb("#dee2e6");
            this["Surface"] = Color.FromArgb("#343a40");
            this["OnSurface"] = Color.FromArgb("#dee2e6");
            this["Outline"] = Color.FromArgb("#495057");
            this["HeadingColor"] = Color.FromArgb("#dee2e6");
            this["HeadingColorAlt"] = Color.FromArgb("#dee2e6");
            this["InputBackground"] = Color.FromArgb("#fff");
            this["InputText"] = Color.FromArgb("#212529");
            this["PlaceholderColor"] = Color.FromArgb("#BF212529");
            this["ProgressBackground"] = Color.FromArgb("#FF464545");
        }
        else
        {
            this["Background"] = Color.FromArgb("#fff");
            this["OnBackground"] = Color.FromArgb("#212529");
            this["Surface"] = Color.FromArgb("#fff");
            this["OnSurface"] = Color.FromArgb("#212529");
            this["Outline"] = Color.FromArgb("#000");
            this["HeadingColor"] = Color.FromArgb("#212529");
            this["HeadingColorAlt"] = Color.FromArgb("#212529");
            this["InputBackground"] = Color.FromArgb("#fff");
            this["InputText"] = Color.FromArgb("#212529");
            this["PlaceholderColor"] = Color.FromArgb("#BF212529");
            this["ProgressBackground"] = Color.FromArgb("#e9ecef");
        }
    }
}
