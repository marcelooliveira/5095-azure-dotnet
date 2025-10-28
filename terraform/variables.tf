variable "resource_group_name" {
  description = "Nome do Resource Group"
  default     = "rg-vollmed-functionapp"
}

variable "location" {
  description = "Região do Azure"
  default     = "eastus2"
}

variable "storage_account_name" {
  description = "Nome da Storage Account (somente minúsculas e números)"
  default     = "vollmedfuncstorage"
}

variable "function_app_name" {
  description = "Nome da Azure Function"
  default     = "vollmed-functionapp"
}

variable "app_service_plan_name" {
  description = "Nome do App Service Plan"
  default     = "plan-vollmed-linux"
}