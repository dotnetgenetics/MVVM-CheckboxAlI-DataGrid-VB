Public Class Products
    Inherits ViewModelBase

    Private _id As Integer

    Public Property Id As Integer
        Get
            Return _id
        End Get

        Set(ByVal value As Integer)
            _id = value
            OnPropertyChanged("Id")
        End Set
    End Property

    Private _name As String

    Public Property ProductName As String
        Get
            Return _name
        End Get

        Set(ByVal value As String)
            _name = value
            OnPropertyChanged("ProductName")
        End Set
    End Property

    Private _price As Decimal

    Public Property UnitPrice As Decimal
        Get
            Return _price
        End Get

        Set(ByVal value As Decimal)
            _price = value
            OnPropertyChanged("UnitPrice")
        End Set
    End Property

    Private _quantity As String

    Public Property QuantityPerUnit As String
        Get
            Return _quantity
        End Get

        Set(ByVal value As String)
            _quantity = value
            OnPropertyChanged("QuantityPerUnit")
        End Set
    End Property

    Private _discontinue As Boolean

    Public Property Discontinue As Boolean
        Get
            Return _discontinue
        End Get

        Set(ByVal value As Boolean)
            _discontinue = value
            OnPropertyChanged("Discontinue")
        End Set
    End Property
End Class