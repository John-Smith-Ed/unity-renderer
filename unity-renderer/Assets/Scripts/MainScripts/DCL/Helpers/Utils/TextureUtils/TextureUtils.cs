using UnityEngine;

//TODO:A.B
/// <summary>
///  did some testing and what this is doing is checking if the image is unity's 'Red Question Mark'
///  Its been years sense i have ran into this issue, but the red qm is unity's default texture to show when a remote image
///  fails to load, I dont know if this is even a real issue anymore in new versions of unity, also the code is checking a texture that is compressed and not read/write enabled
///  which would cause this to fail.
///  So is it never used? Or is it caussing one of the random rouge crashes?
///  TODO: Verify if we need something like this and do it better/smarter?
///  Also note its ran against a image in the build not a remote image, very strange indeed!
/// </summary>


public static class TextureUtils
{
    static readonly byte[] questionMarkPNG = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 8, 0, 0, 0, 8, 8, 2, 0, 0, 0, 75, 109, 41, 220, 0, 0, 0, 65, 73, 68, 65, 84, 8, 29, 85, 142, 81, 10, 0, 48, 8, 66, 107, 236, 254, 87, 110, 106, 35, 172, 143, 74, 243, 65, 89, 85, 129, 202, 100, 239, 146, 115, 184, 183, 11, 109, 33, 29, 126, 114, 141, 75, 213, 65, 44, 131, 70, 24, 97, 46, 50, 34, 72, 25, 39, 181, 9, 251, 205, 14, 10, 78, 123, 43, 35, 17, 17, 228, 109, 164, 219, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130, };
    public static bool IsQuestionMarkPNG(Texture tex)
    {
        if (!tex)
            return true;

        if (tex.width != 8 || tex.height != 8)
            return false;

        byte[] png1 = (tex as Texture2D).EncodeToPNG();
        for (int i = 0; i < questionMarkPNG.Length; i++)
            if (!png1[i].Equals(questionMarkPNG[i]))
                return false;
        return true;
    }
}