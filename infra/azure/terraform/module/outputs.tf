output "function_principal_id" {
  value = azurerm_app_service.amphitrite.identity[0].principal_id
}

output "appinsights_instrumentation_key" {
  value     = var.create_app_insights ? azurerm_application_insights.amphitrite[0].instrumentation_key : var.app_insight_instrumentation_key
  sensitive = true
}