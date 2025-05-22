cd "C:\Personal\uni\master\sem2\ac-lab\streamphony-asp-net"

Write-Host "`nBuilding backend image..." -ForegroundColor Cyan
nerdctl build -t localhost/api -f Dockerfile .

Write-Host "`nSaving backend image..." -ForegroundColor Cyan
nerdctl save localhost/api -o api.tar

Write-Host "`nLoading backend image into Kubernetes..." -ForegroundColor Cyan
nerdctl --namespace k8s.io load -i api.tar

Write-Host "`nBuilding frontend image..." -ForegroundColor Cyan
nerdctl build -t localhost/frontend -f ./Streamphony.React/Dockerfile ./Streamphony.React

Write-Host "`nSaving frontend image..." -ForegroundColor Cyan
nerdctl save localhost/frontend -o frontend.tar

Write-Host "`nLoading frontend image into Kubernetes..." -ForegroundColor Cyan
nerdctl --namespace k8s.io load -i frontend.tar

Write-Host "`nDeleting old deployments and services..." -ForegroundColor Yellow
kubectl delete deployment backend frontend mssql azurite --ignore-not-found
kubectl delete service backend frontend mssql azurite --ignore-not-found

Write-Host "`nDeleting old pods..." -ForegroundColor Yellow
kubectl delete pod -l app=backend --ignore-not-found
kubectl delete pod -l app=frontend --ignore-not-found
kubectl delete pod -l app=mssql --ignore-not-found
kubectl delete pod -l app=azurite --ignore-not-found

Write-Host "`nApplying Kubernetes manifests..." -ForegroundColor Green
kubectl apply -f k8s/sql-deployment.yaml
kubectl apply -f k8s/backend-deployment.yaml
kubectl apply -f k8s/frontend-deployment.yaml
kubectl apply -f k8s/azurite-deployment.yaml

Write-Host "`nDeployment complete. Current pod status:" -ForegroundColor Green
kubectl get pods
