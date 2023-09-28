
Imports System.ComponentModel

Public Class ViewModelBase : Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler _
         Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Class


