Imports System.Collections.ObjectModel
Imports System.Data

Public Class ProductsViewModel
    Inherits ViewModelBase

    #Region "Commands"

    Private _updateItemCommand As ICommand
    Public ReadOnly Property UpdateItemCommand As ICommand
        Get
            If _updateItemCommand Is Nothing Then
                _updateItemCommand = New RelayCommand(Sub(param) UpdateItem(param), Nothing)
            End If

            Return _updateItemCommand
        End Get
    End Property

    Private _updateAllCommand As ICommand
    Public ReadOnly Property UpdateAllCommand As ICommand
        Get
            If _updateAllCommand Is Nothing Then
                _updateAllCommand = New RelayCommand(Sub(param) UpdateAllProducts(param), Nothing)
            End If

            Return _updateAllCommand
        End Get
    End Property

    #End Region

    #Region "Members"
    Public Property MultiParamConverter As MultiParamConverter

    Private _productList As ObservableCollection(Of Products)
    Public Property ProductList As ObservableCollection(Of Products)
        Get
            Return _productList
        End Get

        Set(ByVal value As ObservableCollection(Of Products))
            _productList = value
            OnPropertyChanged("ProductList")
        End Set
    End Property

    Private _checkAll As Boolean
    Public Property CheckAll As Boolean
        Get
            Return _checkAll
        End Get

        Set(ByVal value As Boolean)
            _checkAll = value
            OnPropertyChanged("CheckAll")
        End Set
    End Property

    #End Region
    
    #Region "ViewModel Methods"
    
    Public Sub New()
        MultiParamConverter = New MultiParamConverter()
        GetAllProducts()
    End Sub

    Private Sub UpdateItem(ByVal param As Object)
        Dim product = CType(param, Products)
        DBUtil.UpdateProductDiscontinue(product.Discontinue, product.Id)
        GetAllProducts()
    End Sub

    Private Sub UpdateAllProducts(ByVal param As Object)
        Dim values = CType(param, Object())
        Dim check = CBool(values(0))
        Dim productsItemCollection = CType(values(1), ItemCollection)
        If productsItemCollection.Count > 0 Then
            For Each item In productsItemCollection
                If item IsNot Nothing Then
                    DBUtil.UpdateProductDiscontinue(check, (CType(item, Products)).Id)
                End If
            Next
        End If

        GetAllProducts()
    End Sub

    Private Sub GetAllProducts()
        Dim flag As Boolean = True
        ProductList = New ObservableCollection(Of Products)(Products(DBUtil.GetProduct()))
        For Each product In ProductList
            If Not product.Discontinue Then
                flag = False
                CheckAll = False
            End If
        Next

        If flag Then
            CheckAll = True
        End If
    End Sub

    Private Function Products(ByVal dt As DataTable) As ObservableCollection(Of Products)

        Dim convertedList =(From rw In dt.AsEnumerable()
                Select New Products() _
                With {
                .Id = Convert.ToInt32(rw("productID")), 
                .ProductName = rw("ProductName").ToString(), 
                .UnitPrice = Convert.ToDecimal(rw("UnitPrice")), 
                .QuantityPerUnit =(rw("QuantityPerUnit")).ToString(), 
                .Discontinue = CBool(rw("Discontinue"))}).ToList()
        Return New ObservableCollection(Of Products)(convertedList)

    End Function
    #End Region
    
End Class
