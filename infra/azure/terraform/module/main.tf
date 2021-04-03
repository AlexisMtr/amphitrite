data "azurerm_resource_group" "rg" {
  name = var.rg_name
}

resource "azurerm_app_service_plan" "amphitrite" {
  name                = "${var.release_name}-amphitrite-splan"
  location            = data.azurerm_resource_group.rg.location
  resource_group_name = data.azurerm_resource_group.rg.name
  kind                = "App"

  sku {
    size = var.sku_size
    tier = var.sku_tier
  }
}

resource "azurerm_app_service" "amphitrite" {
  name                = "${var.release_name}-amphitrite"
  location            = data.azurerm_resource_group.rg.location
  resource_group_name = data.azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.amphitrite.id

  site_config {
    dotnet_framework_version = "v5.0"
  }

  app_settings = {
    IssuerSigningKey__SigningKey = var.issuer_signing_key
    ASPNETCORE_ENVIRONMENT       = var.aspnet_environment
  }

  connection_string {
    name  = "DefaultConnection"
    value = var.db_connection_string
    type  = "SQLServer"
  }

  identity {
    type = "SystemAssigned"
  }
}

resource "azurerm_application_insights" "amphitrite" {
  count = var.create_app_insights ? 1 : 0

  name                = "${var.release_name}-amphitrite-appinsights"
  location            = data.azurerm_resource_group.rg.location
  resource_group_name = data.azurerm_resource_group.rg.name
  application_type    = "web"
}