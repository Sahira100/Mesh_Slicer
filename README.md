# Unity 3D Object Slicer

A Unity plugin that enables real-time mesh slicing of 3D objects. This tool allows you to dynamically cut 3D meshes along arbitrary planes while maintaining mesh integrity, UV mapping, and proper face generation.

<div align="center">
  <div style="display: flex;  justify-content: center; gap:30px;">
    <img src="Assets/UI/Screenshot%202024-12-22%20115316.png" alt="Initial Object" width="250"/>
    <img src="Assets/UI/Screenshot 2024-12-22 115423.png" alt="Slicing Process" width="250"/>
  </div>
</div>

## Features

- Real-time mesh slicing along any plane
- Maintains UV mapping and texture coordinates
- Generates proper mesh faces at cut surfaces
- Supports normal interpolation for smooth transitions
- Automatic triangulation of cut faces
- Translation animation for sliced parts
- Area calculation for slice validation
- Local to world space conversion handling

## How It Works

The slicer works by:
1. Detecting intersection points between the slice plane and mesh triangles
2. Splitting affected triangles and creating new vertices
3. Generating new UV coordinates through interpolation
4. Creating proper faces for the cut surface
5. Maintaining mesh normals for correct lighting
6. Separating the mesh into two distinct parts

## Installation

1. Clone this repository or download the latest release
2. Import the package into your Unity project
3. Attach the `SliceMe` script to any mesh you want to make sliceable
4. Configure the slicing parameters in the inspector

## Usage

```csharp
// To make an object sliceable
[RequireComponent(typeof(MeshFilter))]
public class SliceMe : MonoBehaviour
{
    // Configure in inspector
    public float translaitonSpeed;
    public float translationDistance;
}

// To perform a slice
public void Slice(SlicerPlane slicePlane)
{
    // Create slice plane and execute cut
    SlicerPlane plane = new SlicerPlane();
    yourSliceableObject.Slice(plane);
}
```

## Configuration

The `SliceMe` component exposes the following properties:
- `CreateslicePart`: Toggle slice part creation
- `translaitonSpeed`: Speed at which sliced parts move
- `translationDistance`: Distance sliced parts travel

## Requirements

- Unity 2019.4 or higher
- Mesh objects must have proper UV mapping
- Objects must have MeshFilter component

## Contributing

1. Fork the repository
2. Create your feature branch
3. Commit your changes
4. Push to the branch
5. Create a new Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details
