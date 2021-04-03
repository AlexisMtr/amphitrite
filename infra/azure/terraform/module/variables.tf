variable "release_name" {
  type        = string
  description = "Name of the release (used to prefix resources)"
}

variable "rg_name" {
  type        = string
  description = "Name of the existing resource group"
}

variable "db_connection_string" {
  type        = string
  sensitive   = true
  description = "ConnectionString to Database"
}

variable "app_insight_instrumentation_key" {
  type        = string
  description = "Existing instrumentationkey"
}

variable "create_app_insights" {
  type        = bool
  default     = false
  description = "Create or use given instrumentationkey"
}

variable "aspnet_environment" {
  type    = string
  default = "Development"
}

variable "issuer_signing_key" {
  type        = string
  description = "Key used to sign user JWT"
}

variable "sku_size" {
  type    = string
  default = "F1"
}

variable "sku_tier" {
  type    = string
  default = "Free"
}