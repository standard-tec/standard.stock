pipeline {
    agent any
    stages {
        stage('Checkout') {
            steps {
					git credentialsId: 'GitHub', url: 'https://github.com/${ORGANIZATION_NAME}/${SERVICE_NAME}.git'
			}     
    }
}