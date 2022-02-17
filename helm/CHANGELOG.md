# [helm-chart-v2.0.1](https://github.com/AlexisMtr/amphitrite/compare/helm-chart-v2.0.0...helm-chart-v2.0.1) (2022-02-17)


### Bug Fixes

* **amphitrite:** bump appVersion to 1.0.2 ([ff81ca9](https://github.com/AlexisMtr/amphitrite/commit/ff81ca913a42748bfb6c22b8bed7d9dc15e4d692))

# [helm-chart-v2.0.0](https://github.com/AlexisMtr/amphitrite/compare/helm-chart-v1.1.1...helm-chart-v2.0.0) (2021-09-18)


### Bug Fixes

* **helm:** secret encoded function ([43c74e3](https://github.com/AlexisMtr/amphitrite/commit/43c74e397c6f9ba82b30c5c0e4cbad7e09214afa))


### Features

* **helm:** remove ability to use existing secret ([efad1ee](https://github.com/AlexisMtr/amphitrite/commit/efad1ee6c439162f1126105c3761c4aca608e6a1))
* **ingress:** support networking.k8s.io/v1 ingress only ([b9c7f07](https://github.com/AlexisMtr/amphitrite/commit/b9c7f079b5e83aa0a1f79670d8a0c0b1cb298bc7))


### BREAKING CHANGES

* **helm:** existingEnvSecret cannot be used anymore
* **ingress:** drop support for old ingress apiVersion (required kubernetes v1.19+)

# [helm-chart-v1.1.1](https://github.com/AlexisMtr/amphitrite/compare/helm-chart-v1.1.0...helm-chart-v1.1.1) (2021-02-27)


### Bug Fixes

* **amphitrite:** bump appVersion to 1.0.1 ([53aa4c7](https://github.com/AlexisMtr/amphitrite/commit/53aa4c7a58953596493dc44e20ca3adffb62fb25))
* **secrets:** quote secrets ([35c54be](https://github.com/AlexisMtr/amphitrite/commit/35c54be096232fc0d6adcd48042003b1e83bcc68))

# [helm-chart-v1.1.0](https://github.com/AlexisMtr/amphitrite/compare/helm-chart-v1.0.0...helm-chart-v1.1.0) (2021-02-27)


### Features

* **helm:** allow to use existing secret to set env ([16e7737](https://github.com/AlexisMtr/amphitrite/commit/16e773780f79528897fe25ba68e46501e14123f7))

# helm-chart-v1.0.0 (2021-02-13)


### Bug Fixes

* **deployment:** probes, indentation and env ([c5b363a](https://github.com/AlexisMtr/amphitrite/commit/c5b363aec9a0c2371edfbe003c79c747b9e21c13))
* **release:** force helm to release ([8afa60c](https://github.com/AlexisMtr/amphitrite/commit/8afa60c5a0d710a8627fd42640dd82654667c3d3))


### Features

* **deployment:** allow to set ASPNET Env ([f9888bf](https://github.com/AlexisMtr/amphitrite/commit/f9888bfeabf3b3d655b57ee52ec768d80b0e3e06))

This file is generated using [SemanticRelease](https://github.com/semantic-release/changelog)
