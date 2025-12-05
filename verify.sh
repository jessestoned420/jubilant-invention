#!/bin/bash

# Verification script for shader file refactor and bug fix

set -e

# 1. Verify that HyperPBRCharacter_4D.shader has been created
if [ ! -f "HyperPBRCharacter_4D.shader" ]; then
    echo "Error: HyperPBRCharacter_4D.shader not found."
    exit 1
fi

# 2. Confirm that the shader code has been removed from Cinematic_ConfrontLucent.cs
if grep -q "Shader \"Milehigh/HyperPBRCharacter_4D\"" "Cinematic_ConfrontLucent.cs"; then
    echo "Error: Shader code still present in Cinematic_ConfrontLucent.cs."
    exit 1
fi

# 3. Check that the SSS bug is corrected in the new .shader file
# The correct order is o.Albedo = albedo.rgb; followed by the SSS block.
# We can check if the line o.Albedo = albedo.rgb; exists before the SSS block.
if ! grep -A 5 "o.Albedo = albedo.rgb;" "HyperPBRCharacter_4D.shader" | grep -q "Subsurface Scattering"; then
    echo "Error: Subsurface Scattering bug not fixed in HyperPBRCharacter_4D.shader."
    exit 1
fi

echo "Verification successful."
