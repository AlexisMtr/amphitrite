@minLength(1)
@maxLength(10)
param releaseName string = take(uniqueString(resourceGroup().id), 5)
@secure()
param sqlServerConnectionString string
@secure()
param issuerSigningKey string
param skuName string = 'F1'
@secure()
param appInsightInstrumentationKey string
@allowed([
  'Production'
  'Development'
])
param aspNetEnv string = 'Development'

var webAppName = '${releaseName}-amphitrite'
var servicePlanName = '${releaseName}-amphitrite-splan'
var appInsightName = '${releaseName}-amphitrite-appinsights'

resource serverFarm 'Microsoft.Web/serverfarms@2020-10-01' = {
  name: servicePlanName
  location: resourceGroup().location
  sku: {
    name: skuName
  }
}

resource webApp 'Microsoft.Web/sites@2020-10-01' = {
  location: resourceGroup().location
  name: webAppName
  kind: 'api'
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: serverFarm.id
    siteConfig: {
      netFrameworkVersion: 'v5.0'
      appSettings: [
        {
          name: 'IssuerSigningKey:SigningKey'
          value: issuerSigningKey
        }
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: aspNetEnv
        }
      ]
      connectionStrings: [
        {
          name: 'DefaultConnection'
          type: 'SQLServer'
          connectionString: sqlServerConnectionString
        }
      ]
    }
  }
}

resource appInsight 'Microsoft.Insights/components@2015-05-01' = if (empty(appInsightInstrumentationKey)) {
  name: appInsightName
  location: resourceGroup().location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    SamplingPercentage: 100
  }
}

output appInsightInstrumentationKey string = empty(appInsightInstrumentationKey) ? appInsight.properties.InstrumentationKey : appInsightInstrumentationKey
output webAppPrincipalId string = webApp.identity.principalId
