# Read JSON file and parse execution order
$testOrder = Get-Content test_order.json | ConvertFrom-Json
foreach ($feature in $testOrder.order) {
    Write-Host "Running Feature: $feature"
    dotnet test --filter FullyQualifiedName~$feature
}
