HyperText 1.4.0 readme
================================================================================
WARNING: PATCH RELEASES ARE NOT OFFICIALLY SUPPORTED. USE AT YOUR OWN RISK.

More and up to date information available at
http://developers.candlelightinteractive.com/


Installing HyperText
--------------------------------------------------------------------------------

Simply import the package and everything should "just work." Watch my video
tutorial online at http://youtu.be/K7yPDQcfQpU to get an overview of all of the
high-level features and usage of the HyperText tool. If you wish to publish
packages that depend on this one, you can do so with the
IS_CANDLELIGHT_HYPERTEXT_AVAILABLE symbol.

If you experience any problems, please open the Unity preferences menu, navigate
to the Candlelight section on the left, select the HyperText tab, and use the
buttons at the bottom to report a bug or visit the support forum.


Using the Package
--------------------------------------------------------------------------------

This package contains code for a custom component derived from Unity's UI.Text,
Candlelight.UI.HyperText; because it inherits directly from UI.Text component,
it can be used anywhere a UI.Text object can be, and is compatible with built-in
effects (Shadow and Outline).

It adds the following features:

- Support for specifying links in the text. A link consists of an <a> tag with a
  "name" attribute and optional "class" attribute (more on classes later). Use
  syntax: <a name="link_id" class="some_class">Some link</a>. Any event
  involving the link will broadcast the link's name, class, and hitbox locations
  in local space (method signature: void (HyperText, HyperText.LinkInfo)). It
  can send events for OnClick, OnPointerEnter, OnPointerExit, OnPress, and
  OnRelease, the latter two of which are good alternatives to OnPointerEnter and
  OnPointerExit, respectively, for touch platforms.
  
- Link callbacks can be enabled and disabled using the Interactable property.

- HyperText components can reference HyperTextStyles objects, which can be
  created from the HyperText inspector directly, or from the menu option
  Assets -> Create -> HyperText Styles. These objects allow you to define
  reusable styles that will override font, font size, font style, and default
  font color. In addition, they allow you to specify the following:
  
      - Default Link Style: Specifies font style and colors for links in text.
        For a link, the base color is the default text color, unless a base
        color override is enabled and specified. Each state color is then the
        specified state color value multiplied by the color multiplier scalar.
        The color tint mode then specifies how this state color blends with the
        base color. In Constant mode, the state color replaces the base color,
        while in additive and multiplicative they are added and multiplied,
        respectively. The fade duration determines how long the tween between
        colors is, while the color tween mode describes what channel values to
        use when blending between state colors. For example, if the base color
        override is enabled and set to green, the tint mode is multiplicative,
        and the tween mode is alpha only, only the alpha channel of each state
        color will be multiplied with that of the green color.
        
      - Link Styles: Specifies link styles for different classes. Each class
        name in the list must be unique. To use a class of link style, simply
        use its name as the argument for the class attribute in the <a> tag.
        
      - Custom Text Styles: Specifies custom tags to apply rich text styling. By
        default, a new HyperTextStyles object will be populated with <sub> and
        <sup>, which can then be used with syntax: E=mc<sup>2</sup>.
        
      - Quad Styles: Specifies properties for quads to be drawn in text. You can
        use quads to include Sprite icons in-line with your text (such as e.g.,
        button faces for controllers). Each class in the list must have a unique
        name, and allows you to use the syntax: <quad class="class_name">. Each
        quad class allows you to specify a Sprite to be used, a toggle to
        indicate whether or not the font color should be applied, vertical
        offset and size properties, as well as optional link properties. For
        example, if you specify a link ID, every instance of that quad class
        will be wrapped in a link with the specified ID for its name attribute.
        If a link ID is specified, you can also specify which link class should
        be used.
        
      - Inherited Styles: Specifies other HyperTextStyles to inherit. For
        example, you could define a set of quads for controller buttons on one
        set of styles, and automatically inherit these quad styles on any number
        of other style objects with different fonts, font sizes, colors, and so
        on. Note that styles are inherited in order, and so any overrides on the
        last style in the list will be used. You can also optionally redefine
        any inherited styles in order to override them.
        
- HyperText supports link and tag keyword collections. By supplying collections
  of keywords, you can have links or custom tags inserted around matching text
  automatically as part of the text postprocessing step. For example, if a link
  keyword collection contains the word "big dinosaur", then any instance of "big
  dinosaur" occurring in the text will automatically become a link. You can use
  this feature to highlight key vocabulary words or colorize item names
  according to rarity, for example. Each keyword collection can only be
  referenced by the component one time, and its first occurrence in the list for
  links will be given precedence.
  
- Support for specifying rich text sizes as a percentage of surrounding text
  size, rather than as a raw value. Use syntax: <size=150%>big text</size>.

- Support for specifying a custom input source. By assigning an ITextSource
  object to the HyperText.InputTextSource property, you can use a dynamic source
  of input text, such as a localization database. See the included
  LocalizableText class for an example.
  
- HyperText and HyperTextStyles fully support undo and multi-object editing.

- HyperText can be subclassed if you need to support custom IVertexModifier
  types. Simply override the method PostprocessCharacterIndexRanges().
  
The package includes documented source code and inspector tooltips throughout.
Please feel free to contact me though if you have any problems, questions, or
other suggestions for improvement!


Known Issues
--------------------------------------------------------------------------------

- The micro mscorlib stripping setting currently does not work.

- Quad sprites packed in an atlas must use rect packing; tight packing is not
  currently supported. The easiest method is to use the "[RECT]" prefix on the
  Packing Tag for any sprites that will be used as quads.

- Because Unity always triggers a geometry update when the inspector changes,
  a link's color will automatically switch when using the inspector toggle to
  enable/disable links. Fade duration is still respected when toggled via code.
  
- All geometry is generated using Unity's TextGenerator, which imposes the
  following restrictions:
  
      - Due to a bug, there are some circumstances under which quads do not
        wrap properly (i.e. will be rendered outside the rect at the end of a
        line of text).
      
      - Due to a bug, differently sized text will apply an incorrect vertical
        offset on the line on which it appears, unless it spans more than one
        line.
      
      - Due to a bug, quads whose width is greater than their height causes
        the line of text to have an incorrect vertical offset.
            
      - Font style and size effects can only be applied to dynamic fonts.
      
      - The innermost <color> tags take precedence. A link wrapped in
        a custom tag will appear using the link color, while a custom tag
        wrapped in a link will appear using the the custom tag color. Quads will
        always use the color of the link style if a link ID is specified for
        them. Otherwise, they will use their own color.
      
      - Any <size> tags or custom styles with a size scale specified (e.g.,
        <sub>, <sup>) will not display as expected if the output is scaled via
        the best fit property.