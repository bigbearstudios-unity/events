# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2020-11-03

### Added

- Added Event, EventSystem, GlobalEventSystem.

## [1.0.1] - 2020-11-03

### Fixed

- Fixed issue where Clear(true) wouldn't preserve lookups.

## [2.0.0] - 2021-01-28

### Changed

- Removed the need for the BBUnity.Events namespace, its now on the BBUnity namespace
- Renamed EventSystem to GameEventSystem to stop clashes with Unity EventSystem
- Renamed LWEventSystem to GameLWEventSystem
- Removed the Global* classes and replaced them with a .Global call on the GameEventSystem and GameLWEventSystem