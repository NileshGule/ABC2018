Param(
    [parameter(Mandatory=$false)][bool]$IsLocalCluster=$true
)

if($IsLocalCluster){

    $minikubeStatus = minikube status

    Write-Host $minikubeStatus -ForegroundColor Magenta

    $IsStopped =$minikubeStatus.Contains("minikube: Stopped")
    
    if($IsStopped)
    {
        Write-Host "Starting local minikube cluster" -ForegroundColor Yellow
        minikube start
    }
}

Write-Host "Starting deployment of TechTalks application and services" -ForegroundColor Yellow
Set-Location ~/projects/ABC2018/Kubernetes

Write-Host "Creating namespace"  -ForegroundColor Yellow
kubectl apply --filename 00_ABCNamespace.yml

Write-Host "Creating storage class" -ForegroundColor Yellow
kubectl apply --filename 01_StorageClass.yml

Write-Host "Creating Persistant Volume Claim" -ForegroundColor Yellow
kubectl apply --filename 02_PersistantVolumeClaim.yml