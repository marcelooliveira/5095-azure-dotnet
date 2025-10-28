output "function_app_name" {
  value = azurerm_linux_function_app.functionapp.name
}

output "function_app_url" {
  value = "https://${azurerm_linux_function_app.functionapp.default_hostname}"
}

output "resource_group" {
  value = azurerm_resource_group.rg.name
}