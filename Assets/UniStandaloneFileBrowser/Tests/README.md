# Unity Standalone File Browser - Test Suite

## Overview

This test suite provides essential unit tests for the Unity Standalone File Browser library, focusing on core functionality and basic parameter validation.

## Test Structure

```
Assets/UniStandaloneFileBrowser/Tests/
└── Runtime/
    ├── USFB.Tests.asmdef                    # Test assembly definition
    ├── StandaloneFileBrowserTests.cs        # Core API tests
    └── ExtensionFilterTests.cs              # ExtensionFilter struct tests
```

## Test Coverage

### StandaloneFileBrowserTests.cs
Tests the main API methods:
- `OpenFilePanel` - File selection dialog
- `OpenFolderPanel` - Folder selection dialog  
- `SaveFilePanel` - Save file dialog

Each method is tested with:
- Valid parameters
- Null parameter handling
- Basic multiselect functionality

### ExtensionFilterTests.cs
Tests the ExtensionFilter struct:
- Constructor with single extension
- Constructor with multiple extensions
- Null parameter handling
- Property access

## Running Tests

### Unity Test Runner
1. Open Unity Editor
2. Go to `Window > General > Test Runner`
3. Select "PlayMode" tab
4. Click "Run All" or select specific tests

### Command Line
```bash
Unity.exe -batchmode -quit -projectPath "path/to/project" -runTests -testPlatform PlayMode
```

## Test Philosophy

This test suite follows a lightweight approach:
- **Essential Coverage**: Tests core functionality without over-engineering
- **Basic Validation**: Ensures methods handle null parameters gracefully
- **Simple Structure**: Easy to understand and maintain
- **No Mocking**: Tests work with the actual implementation

## Expected Results

All tests should pass, demonstrating that:
- API methods return expected types (FileInfo[], DirectoryInfo[], string)
- Null parameters are handled without exceptions
- ExtensionFilter struct works correctly
- Basic multiselect functionality is available

## Maintenance

- Add new tests when adding new API methods
- Keep tests simple and focused on core functionality
- Update this README when making structural changes
- Ensure tests remain lightweight and maintainable

This simplified test suite provides confidence in the core functionality while remaining easy to understand and maintain.
