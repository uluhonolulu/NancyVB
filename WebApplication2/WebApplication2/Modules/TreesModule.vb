﻿Imports Nancy.ModelBinding
Imports WebApplication2.Services
Imports WebApplication2.Model
Imports Nancy

Namespace Modules

    Public Class TreesModule
        Inherits NancyModule

        Public Sub New(ByVal treeService As TreeService)
            MyBase.Get("/trees") = Function(parameters)
                                       Return View(New TreesModel() With {.Trees = treeService.AllTrees()})

                                   End Function
            MyBase.Get("/trees/{Id}") = Function(parameters)
                                            Dim result As Integer
                                            Dim isInteger = Integer.TryParse(parameters.id, result)
                                            Dim tree = treeService.FindById(result)
                                            If isInteger AndAlso tree IsNot Nothing Then
                                                Return View(tree)
                                            Else
                                                Return HttpStatusCode.NotFound
                                            End If
                                        End Function
            MyBase.Get("/trees/add/") = Function(parameters)
                                            Return View("AddTree.vbhtml", New TreeModel)
                                        End Function
            MyBase.Post("/trees/add/") = Function(parameters)
                                             Dim tree = Me.Bind(Of TreeModel)()
                                             treeService.Add(tree)
                                             Return Response.AsRedirect("/trees")
                                         End Function

        End Sub

    End Class
End Namespace