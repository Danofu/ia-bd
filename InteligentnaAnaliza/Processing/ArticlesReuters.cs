
// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Articles
{

    private ArticlesREUTERS[] rEUTERSField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("REUTERS")]
    public ArticlesREUTERS[] REUTERS
    {
        get
        {
            return this.rEUTERSField;
        }
        set
        {
            this.rEUTERSField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ArticlesREUTERS
{

    private string dATEField;

    private string mKNOTEField;

    private string[] tOPICSField;

    private string[] pLACESField;

    private string[] pEOPLEField;

    private string[] oRGSField;

    private string[] eXCHANGESField;

    private object cOMPANIESField;

    private string uNKNOWNField;

    private ArticlesREUTERSTEXT tEXTField;

    private string tOPICS1Field;

    private string lEWISSPLITField;

    private string cGISPLITField;

    private ushort oLDIDField;

    private ushort nEWIDField;

    private uint cSECSField;

    private bool cSECSFieldSpecified;

    /// <remarks/>
    public string DATE
    {
        get
        {
            return this.dATEField;
        }
        set
        {
            this.dATEField = value;
        }
    }

    /// <remarks/>
    public string MKNOTE
    {
        get
        {
            return this.mKNOTEField;
        }
        set
        {
            this.mKNOTEField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("D", IsNullable = false)]
    public string[] TOPICS
    {
        get
        {
            return this.tOPICSField;
        }
        set
        {
            this.tOPICSField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("D", IsNullable = false)]
    public string[] PLACES
    {
        get
        {
            return this.pLACESField;
        }
        set
        {
            this.pLACESField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("D", IsNullable = false)]
    public string[] PEOPLE
    {
        get
        {
            return this.pEOPLEField;
        }
        set
        {
            this.pEOPLEField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("D", IsNullable = false)]
    public string[] ORGS
    {
        get
        {
            return this.oRGSField;
        }
        set
        {
            this.oRGSField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("D", IsNullable = false)]
    public string[] EXCHANGES
    {
        get
        {
            return this.eXCHANGESField;
        }
        set
        {
            this.eXCHANGESField = value;
        }
    }

    /// <remarks/>
    public object COMPANIES
    {
        get
        {
            return this.cOMPANIESField;
        }
        set
        {
            this.cOMPANIESField = value;
        }
    }

    /// <remarks/>
    public string UNKNOWN
    {
        get
        {
            return this.uNKNOWNField;
        }
        set
        {
            this.uNKNOWNField = value;
        }
    }

    /// <remarks/>
    public ArticlesREUTERSTEXT TEXT
    {
        get
        {
            return this.tEXTField;
        }
        set
        {
            this.tEXTField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute("TOPICS")]
    public string TOPICS1
    {
        get
        {
            return this.tOPICS1Field;
        }
        set
        {
            this.tOPICS1Field = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string LEWISSPLIT
    {
        get
        {
            return this.lEWISSPLITField;
        }
        set
        {
            this.lEWISSPLITField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string CGISPLIT
    {
        get
        {
            return this.cGISPLITField;
        }
        set
        {
            this.cGISPLITField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort OLDID
    {
        get
        {
            return this.oLDIDField;
        }
        set
        {
            this.oLDIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort NEWID
    {
        get
        {
            return this.nEWIDField;
        }
        set
        {
            this.nEWIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint CSECS
    {
        get
        {
            return this.cSECSField;
        }
        set
        {
            this.cSECSField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool CSECSSpecified
    {
        get
        {
            return this.cSECSFieldSpecified;
        }
        set
        {
            this.cSECSFieldSpecified = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ArticlesREUTERSTEXT
{

    private string tITLEField;

    private string aUTHORField;

    private string dATELINEField;

    private string bODYField;

    private string[] textField;

    private string tYPEField;

    /// <remarks/>
    public string TITLE
    {
        get
        {
            return this.tITLEField;
        }
        set
        {
            this.tITLEField = value;
        }
    }

    /// <remarks/>
    public string AUTHOR
    {
        get
        {
            return this.aUTHORField;
        }
        set
        {
            this.aUTHORField = value;
        }
    }

    /// <remarks/>
    public string DATELINE
    {
        get
        {
            return this.dATELINEField;
        }
        set
        {
            this.dATELINEField = value;
        }
    }

    /// <remarks/>
    public string BODY
    {
        get
        {
            return this.bODYField;
        }
        set
        {
            this.bODYField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string[] Text
    {
        get
        {
            return this.textField;
        }
        set
        {
            this.textField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string TYPE
    {
        get
        {
            return this.tYPEField;
        }
        set
        {
            this.tYPEField = value;
        }
    }
}

