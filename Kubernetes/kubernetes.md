# Kubernetes

This directory contains a set of Kubernetes manifest files required for deploying the TechTalks application to Kubernetes cluster. The names of the sub folders are self explanatory.

## Note

Due to the resource requirements for different Docker images, it is advisable to deploy the components in following order

1 - 00_ABCNamespace.yml
2 - 01_StorageClass.yml
3 - 02_PersistantVolumeClaim.yml