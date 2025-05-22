#!binbash

set -e

echo 🚧 Билдим backend (api)...
nerdctl build -t api -f .Streamphony.WebAPIDockerfile .Streamphony.WebAPI

echo 🚧 Билдим frontend...
nerdctl build -t frontend -f .Streamphony.ReactDockerfile .Streamphony.React

echo ✅ Список образов
nerdctl images  grep -E 'apifrontend'

echo 🧹 Удаляем старые деплойменты...
kubectl delete deployment backend frontend --ignore-not-found

echo 🚀 Применяем YAML'ы...
kubectl apply -f .k8ssql-deployment.yaml
kubectl apply -f .k8sbackend-deployment.yaml
kubectl apply -f .k8sfrontend-deployment.yaml

echo 🕒 Ждём запуска подов...
kubectl wait --for=condition=Available deploymentbackend --timeout=60s
kubectl wait --for=condition=Available deploymentfrontend --timeout=60s

echo 🌐 Готово!
echo ✅ Backend httplocalhost5207swagger
echo ✅ Frontend httplocalhost3000

echo 📡 Открываем порты...
echo (Не закрывай это окно, пока тестируешь)

# Прокинуть порты в фоне
kubectl port-forward servicebackend 52075207 &
kubectl port-forward servicefrontend 300080 &

wait
