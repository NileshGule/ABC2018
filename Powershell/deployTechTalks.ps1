Write-Host "Starting deployment of TechTalks application and services"  -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes

Write-Host "Creating namespace"  -ForegroundColor Yellow
kubectl apply 00_ABCNamespace.yml

Write-Host "Creating storage class" -ForegroundColor Yellow
kubectl apply 01_StorageClass.yml

Write-Host "Creating Persistant Volume Claim" -ForegroundColor Yellow
kubectl apply 02_PersistantVolumeClaim.yml