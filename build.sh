sudo docker build -t curcas/centralbackup:latest .
sudo docker tag curcas/centralbackup:latest curcas/centralbackup:0.1
sudo docker push curcas/centralbackup:latest
sudo docker push curcas/centralbackup:0.1