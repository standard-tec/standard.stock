pipeline {
    agent any
    stages {
        stage('build') {
            steps {
                git credentialsId: 'GitHub', url: 'https://github.com/${ORGANIZATION_NAME}/${SERVICE_NAME}.git'
            }
        }        
    }
}