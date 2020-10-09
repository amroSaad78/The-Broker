
  Write-Host "Cleaning previous helm releases..." -ForegroundColor Green
  helm delete $(helm ls -q --filter 'brok[a-z]+') 
  Write-Host "Previous releases deleted" -ForegroundColor Green