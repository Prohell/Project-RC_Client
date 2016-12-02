#coding=utf-8

import sys
import os
import re
 
global configuration
global workspace

def main():
  if len(sys.argv)<2:
        print "Passing 2 arg for configuration and workspace."
        return False

  configuration =sys.argv[1]
  workspace=sys.argv[2]

  f = open(workspace+"/release/iOS/Unity-iPhone.xcodeproj/project.pbxproj","r")
  filedata = f.read()
  f.close()

  new1data = filedata.replace('5623C57217FDCB0800090B9E = {\n\t\t\t\t\t\tTestTargetID = 1D6058900D05DD3D006BFB54;\n\t\t\t\t\t};'
    , '1D6058900D05DD3D006BFB54 = {\n\t\t\t\t\t\tDevelopmentTeam = 639S9R72A5;\n\t\t\t\t\t\tProvisioningStyle = Manual;\n\t\t\t\t\t};')
  new2data = new1data.replace('PRODUCT_NAME = rc;'
    , 'PRODUCT_NAME = rc;\n\t\t\t\tPROVISIONING_PROFILE = "0dd83867-9ca7-4545-97a9-1a1209f3a106";\n\t\t\t\tPROVISIONING_PROFILE_SPECIFIER = "cabal-inhouse";')
  new3data = new2data.replace('CLANG_WARN_DEPRECATED_OBJC_IMPLEMENTATIONS = YES;\n\t\t\t\tCOPY_PHASE_STRIP = YES;'
    , 'CLANG_WARN_DEPRECATED_OBJC_IMPLEMENTATIONS = YES;\n\t\t\t\t"CODE_SIGN_IDENTITY[sdk=iphoneos*]" = "iPhone Distribution";\n\t\t\t\tCOPY_PHASE_STRIP = YES;\n\t\t\t\tDEVELOPMENT_TEAM = 639S9R72A5;')
  new4data = new3data.replace('CLANG_WARN_DEPRECATED_OBJC_IMPLEMENTATIONS = YES;\n\t\t\t\tCOPY_PHASE_STRIP = NO;'
    , 'CLANG_WARN_DEPRECATED_OBJC_IMPLEMENTATIONS = YES;\n\t\t\t\t"CODE_SIGN_IDENTITY[sdk=iphoneos*]" = "iPhone Distribution";\n\t\t\t\tCOPY_PHASE_STRIP = NO;\n\t\t\t\tDEVELOPMENT_TEAM = 639S9R72A5;')
  new5data = new4data.replace('ENABLE_BITCODE = YES;'
    , 'ENABLE_BITCODE = NO;')

  f = open(workspace+"/release/iOS/Unity-iPhone.xcodeproj/project.pbxproj","w")
  f.write(new5data)
  f.close()

main()